using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BPOR.Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NIHR.Rts.Client.Settings;

namespace NIHR.Rts.Client;

public class RtsAddressSource : IRtsAddressSource
{
    private readonly ITokenService _tokenService;
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly RtsApiSettings _settings;
    private sealed record PostcodeSearchCacheKey(Postcode Postcode);
    private sealed record AddressLookupCacheKey(string RtsAddressId);

    public RtsAddressSource(
        ITokenService tokenService,
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
                       entry.AbsoluteExpirationRelativeToNow =
                           TimeSpan.FromMinutes(
                               Convert.ToDouble(_settings.AddressCacheTimeSpanMinutes));

                       var response = await SendWithTokenRetryAsync(
                           $"api/v2/Rts/GetOrganisationList?postcode={postcode}",
                           cancellationToken);

                       var result = await response.Content.ReadFromJsonAsync<RtsResponse>(
                           cancellationToken: cancellationToken);

                       return result?.Result.RtsOrganisations?.ToArray()
                              ?? Array.Empty<RtsAddress>();
                   })
               ?? Array.Empty<RtsAddress>();
    }

    public async Task<RtsAddress?> GetById(
        string rtsAddressId,
        CancellationToken cancellationToken)
    {
        return await _cache.GetOrCreateAsync(
            new AddressLookupCacheKey(rtsAddressId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(
                        Convert.ToDouble(_settings.AddressCacheTimeSpanMinutes));

                var response = await SendWithTokenRetryAsync(
                    $"api/v2/Rts/GetOrganisationList?identifier={Uri.EscapeDataString(rtsAddressId)}",
                    cancellationToken);

                var result = await response.Content.ReadFromJsonAsync<RtsResponse>(
                    cancellationToken: cancellationToken);

                return result?.Result.RtsOrganisations.SingleOrDefault();
            });
    }
    
    private async Task<HttpResponseMessage> SendWithTokenRetryAsync(
        string requestUri,
        CancellationToken cancellationToken)
    {
        var token = await _tokenService.GetAccessTokenAsync(cancellationToken);

        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            requestUri);

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(
            request,
            cancellationToken);

        if (response.StatusCode is HttpStatusCode.Unauthorized
            or HttpStatusCode.Forbidden)
        {
            response.Dispose();

            token = await _tokenService.RefreshAccessTokenAsync(
                cancellationToken);

            using var retryRequest = new HttpRequestMessage(
                HttpMethod.Get,
                requestUri);

            retryRequest.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            response = await _httpClient.SendAsync(
                retryRequest,
                cancellationToken);
        }

        response.EnsureSuccessStatusCode();

        return response;
    }
}