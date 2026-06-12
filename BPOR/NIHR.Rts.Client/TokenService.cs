using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NIHR.Rts.Client;
using NIHR.Rts.Client.Settings;

public class TokenService : ITokenService
{
    private readonly HttpClient _httpClient;
    private readonly RtsApiSettings _settings;
    private readonly IMemoryCache _cache;
    private const string _cacheKey = "rts-access-token";


    public TokenService(HttpClient httpClient, IOptions<RtsApiSettings> settings, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _cache = cache;
    }
    
    public async Task<string> GetAccessTokenAsync(
        CancellationToken cancellationToken)
    {
        return await _cache.GetOrCreateAsync(
            _cacheKey,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(_settings.TokenCacheTtlMinutes);

                return await RequestTokenAsync(cancellationToken);
            }) ?? throw new InvalidOperationException("Failed to obtain token");
    }

    public async Task<string> RefreshAccessTokenAsync(
        CancellationToken cancellationToken)
    {
        _cache.Remove(_cacheKey);

        return await GetAccessTokenAsync(cancellationToken);
    }

    public async Task<string> RequestTokenAsync(CancellationToken cancellationToken)
    {
        var request = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", _settings.ClientId),
            new KeyValuePair<string, string>("client_secret", _settings.ClientSecret),
            new KeyValuePair<string, string>("scope", "openid profile email")
        });

        var response = await _httpClient.PostAsync("oauth2/token", request, cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);

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