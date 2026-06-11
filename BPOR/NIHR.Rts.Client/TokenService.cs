using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Options;
using NIHR.Rts.Client.Settings;

public class TokenService
{
    private readonly HttpClient _httpClient;
    private readonly RtsApiSettings _settings;

    public TokenService(HttpClient httpClient, IOptions<RtsApiSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var request = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", _settings.ClientId),
            new KeyValuePair<string, string>("client_secret", _settings.ClientSecret),
            new KeyValuePair<string, string>("scope", "openid profile email")
        });

        var response = await _httpClient.PostAsync("oauth2/token", request);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(json);

        return tokenResponse!.access_token;
    }
}

public class TokenResponse
{
    public string access_token { get; set; } = string.Empty;
    public string token_type { get; set; } = string.Empty;
    public int expires_in { get; set; }
}