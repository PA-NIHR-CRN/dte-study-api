using System.Net.Http.Json;
using BPOR.Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NIHR.Rts.Client.Settings;

namespace NIHR.Rts.Client;

public class RtsAddressSource : IRtsAddressSource
{
    private readonly TokenService _tokenService;
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly RtsApiSettings _settings;
    private sealed record PostcodeSearchCacheKey(Postcode Postcode);
    private sealed record AddressLookupCacheKey(string RtsAddressId);

    public RtsAddressSource(
        TokenService tokenService,
        HttpClient httpClient,
        IMemoryCache cache,
        IOptions<RtsApiSettings> settings)
    {
        _tokenService = tokenService;
        _httpClient = httpClient;
        _cache = cache;
        _settings = settings.Value;
    }

    public async Task<IEnumerable<RtsAddress>> SearchByPostcode(
        Postcode postcode,
        CancellationToken cancellationToken)
    {
        return await _cache.GetOrCreateAsync(
            new PostcodeSearchCacheKey(postcode),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(Convert.ToDouble(_settings.CacheTimeSpanMinutes));

                var accessToken = await _tokenService.GetAccessTokenAsync(cancellationToken);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer",
                        accessToken);

                var response = await _httpClient.GetAsync(
                    $"api/v2/Rts/GetOrganisationList?postcode={postcode}",
                    cancellationToken);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<RtsResponse>(
                    cancellationToken: cancellationToken);

                return result?.Result.RtsOrganisations?.ToArray()
                       ?? Array.Empty<RtsAddress>();
            }) ?? Array.Empty<RtsAddress>();
    }

    public async Task<RtsAddress?> GetById(
        string rtsAddressId,
        CancellationToken cancellationToken)
    {
        return await _cache.GetOrCreateAsync(
            new AddressLookupCacheKey(rtsAddressId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(Convert.ToDouble(_settings.CacheTimeSpanMinutes));

                var accessToken = await _tokenService.GetAccessTokenAsync(cancellationToken);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer",
                        accessToken);

                var response = await _httpClient.GetAsync(
                    $"api/v2/Rts/GetOrganisationList?identifier={Uri.EscapeDataString(rtsAddressId)}",
                    cancellationToken);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<RtsResponse>(
                    cancellationToken: cancellationToken);

                return result?.Result.RtsOrganisations.SingleOrDefault();
            });
    }
}