using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;
using System.Text;
using FormBuilder.Interface;

namespace FormBuilder.Controllers
{
    public class SalesforceController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISalesForceService _salesForceService;
        public SalesforceController(IConfiguration config, IHttpClientFactory httpClientFactory, ISalesForceService salesForceService)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _salesForceService = salesForceService;
        }

        private static string GenerateCodeVerifier()
        {
            var random = new byte[32];
            RandomNumberGenerator.Fill(random);
            return Base64UrlEncode(random);
        }

        private static string GenerateCodeChallenge(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.ASCII.GetBytes(codeVerifier));
            return Base64UrlEncode(bytes);
        }

        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        public IActionResult Login()
        {
            var clientId = _config["Salesforce:ClientId"];
            var redirectUri = _config["Salesforce:CallbackUrl"];

            var codeVerifier = GenerateCodeVerifier();
            var codeChallenge = GenerateCodeChallenge(codeVerifier);

            HttpContext.Session.SetString("code_verifier", codeVerifier);

            var authUrl = $"https://login.salesforce.com/services/oauth2/authorize" +
                          $"?response_type=code" +
                          $"&client_id={clientId}" +
                          $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
                          $"&code_challenge={codeChallenge}" +
                          $"&code_challenge_method=S256";

            return Redirect(authUrl);
        }

        public async Task<IActionResult> Callback(string code, string error, string error_description)
        {
            if (!string.IsNullOrEmpty(error))
            {
                TempData["Error"] = $"Salesforce OAuth error: {error} - {error_description}";
                return RedirectToAction("Dashboard", "User");
            }

            if (string.IsNullOrEmpty(code))
            {
                TempData["Error"] = "No code returned from Salesforce.";
                return RedirectToAction("Dashboard", "User");
            }

            var codeVerifier = HttpContext.Session.GetString("code_verifier");
            if (string.IsNullOrEmpty(codeVerifier))
            {
                TempData["Error"] = "Code verifier missing from session.";
                return RedirectToAction("Dashboard", "User");
            }

            var client = _httpClientFactory.CreateClient();

            var tokenRequest = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "client_id", _config["Salesforce:ClientId"] },
                { "client_secret", _config["Salesforce:ClientSecret"] },
                { "redirect_uri", _config["Salesforce:CallbackUrl"] },
                { "code_verifier", codeVerifier }
            };

            var response = await client.PostAsync("https://login.salesforce.com/services/oauth2/token", new FormUrlEncodedContent(tokenRequest));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Salesforce token exchange failed: " + content;
                return RedirectToAction("Dashboard", "User");
            }

            var json = JsonDocument.Parse(content).RootElement;
            var accessToken = json.GetProperty("access_token").GetString();
            var instanceUrl = json.GetProperty("instance_url").GetString();

            HttpContext.Session.SetString("SalesforceAccessToken", accessToken);
            HttpContext.Session.SetString("SalesforceInstanceUrl", instanceUrl);

            TempData["Message"] = "Connected to Salesforce successfully!";
            return RedirectToAction("Dashboard", "User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disconnect()
        {
            
            var AccessToken = HttpContext.Session.GetString("SalesforceAccessToken");
   
            HttpContext.Session.Remove("SalesforceInstanceUrl");

            await _salesForceService.RevokeToken(AccessToken);

            HttpContext.Session.Remove("SalesforceAccessToken");
            TempData["Message"] = "Disconnected from Salesforce successfully.";
            return RedirectToAction("Dashboard", "User");
        }
    }
}
