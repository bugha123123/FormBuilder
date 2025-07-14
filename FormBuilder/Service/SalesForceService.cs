using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using FormBuilder.Data;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class SalesForceService : ISalesForceService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AppDbContext _context;
    private readonly IAuthService _authService;
    public SalesForceService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, AppDbContext context, IAuthService authService)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClientFactory = httpClientFactory;
        _context = context;
        _authService = authService;
    }

    public async Task<(bool success, string error)> CreateSalesforceAccountAndContact(string companyName, string phoneNumber, string useCase)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return (false, "HttpContext is null");

        var user = await _authService.GetLoggedInUserAsync();
        if (string.IsNullOrEmpty(user.Id)) return (false, "User not authenticated");

        var accessToken = httpContext.Session.GetString("SalesforceAccessToken");
        var instanceUrl = httpContext.Session.GetString("SalesforceInstanceUrl");

        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(instanceUrl))
            return (false, "Salesforce is not connected. Please log in first.");

      
        //var templateCount = _context.FormTemplates.Count(t => t.UserId == user.Id);
        //var formCount = _context.Forms.Count(f => f.UserId == user.Id);
        //var questionCount = _context.Questions.Count(q => q.Template.UserId == user.Id);
        //var commentCount = _context.Comments.Count(c => c.UserId == user.Id);

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var accountPayload = new
        {
            Name = companyName,
            Phone = phoneNumber,
            Description = $"Use Case: {useCase}",
            //Templates_Count__c = templateCount,      
            //Forms_Count__c = formCount,
            //Questions_Count__c = questionCount,
            //Comments_Count__c = commentCount
        };
        var accountContent = new StringContent(JsonSerializer.Serialize(accountPayload));
        accountContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var accountResponse = await client.PostAsync($"{instanceUrl}/services/data/v58.0/sobjects/Account", accountContent);
        if (!accountResponse.IsSuccessStatusCode)
        {
            var err = await accountResponse.Content.ReadAsStringAsync();
            return (false, $"Account error: {err}");
        }

        var accountResult = JsonDocument.Parse(await accountResponse.Content.ReadAsStringAsync());
        var accountId = accountResult.RootElement.GetProperty("id").GetString();

        var contactPayload = new
        {
            FirstName = "API User",
            LastName = companyName,
            Phone = phoneNumber,
            AccountId = accountId
        };

        var contactContent = new StringContent(JsonSerializer.Serialize(contactPayload));
        contactContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var contactResponse = await client.PostAsync($"{instanceUrl}/services/data/v58.0/sobjects/Contact", contactContent);
        if (!contactResponse.IsSuccessStatusCode)
        {
            var err = await contactResponse.Content.ReadAsStringAsync();
            return (false, $"Contact error: {err}");
        }

        return (true, null);
    }

    public async Task RevokeToken(string accessToken)
    {
        var client = _httpClientFactory.CreateClient();
        var revokeUrl = $"https://login.salesforce.com/services/oauth2/revoke?token={accessToken}";
        await client.GetAsync(revokeUrl);
    }
}
