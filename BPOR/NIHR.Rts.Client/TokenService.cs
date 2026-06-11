using System.Net;
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
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(Convert.ToDouble(_settings.TokenCacheTimeSpanHours));

                return await RequestTokenAsync(cancellationToken);
            }) ?? throw new InvalidOperationException("Failed to obtain token");
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
        
        if (response.StatusCode is HttpStatusCode.Unauthorized
            or HttpStatusCode.Forbidden)
        {
            _cache.Remove(_cacheKey);

            throw new UnauthorizedAccessException(
                "Token request was unauthorized (401/403). Cache invalidated.");
        }

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(json);
        
        if (tokenResponse?.access_token is null)
        {
            _cache.Remove(_cacheKey);

            throw new InvalidOperationException("Token response was invalid.");
        }

        return tokenResponse.access_token;
    }
}

public class TokenResponse
{
    public string access_token { get; set; } = string.Empty;
    public string token_type { get; set; } = string.Empty;
    public int expires_in { get; set; }
}