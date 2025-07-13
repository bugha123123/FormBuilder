using Dropbox.Api;
using Dropbox.Api.Files;
using FormBuilder.Interface;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class DropboxService : IDropboxService
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _refreshToken;
    private string _cachedAccessToken;
    private DateTime _accessTokenExpiry = DateTime.MinValue;

    public DropboxService(IConfiguration config)
    {
        _clientId = config["Dropbox:ClientId"];
        _clientSecret = config["Dropbox:ClientSecret"];
        _refreshToken = config["Dropbox:RefreshToken"];
    }

    public async Task<bool> UploadJsonFileAsync(string fileName, string jsonContent)
    {
        try
        {
            var accessToken = await GetValidAccessTokenAsync();

            using var dbx = new DropboxClient(accessToken);
            using var memStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));

            await dbx.Files.UploadAsync(
                path: $"/Support/{fileName}",
                mode: WriteMode.Overwrite.Instance,
                body: memStream);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Dropbox upload failed: " + ex.Message);
            return false;
        }
    }

    private async Task<string> GetValidAccessTokenAsync()
    {
        if (!string.IsNullOrEmpty(_cachedAccessToken) && _accessTokenExpiry > DateTime.UtcNow.AddMinutes(1))
        {
            return _cachedAccessToken;
        }

        using var client = new HttpClient();
        var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.dropbox.com/oauth2/token");
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", _refreshToken }
        });

        var response = await client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to refresh Dropbox token: " + json);
        }

        using var doc = JsonDocument.Parse(json);
        _cachedAccessToken = doc.RootElement.GetProperty("access_token").GetString();
        var expiresIn = doc.RootElement.GetProperty("expires_in").GetInt32();
        _accessTokenExpiry = DateTime.UtcNow.AddSeconds(expiresIn);

        return _cachedAccessToken;
    }
}
