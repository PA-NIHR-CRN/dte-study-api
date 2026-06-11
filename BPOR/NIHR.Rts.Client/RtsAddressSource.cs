using System.Net.Http.Json;

namespace NIHR.Rts.Client;

public class RtsAddressSource : IRtsAddressSource
{
    private readonly TokenService _tokenService;
    private readonly HttpClient _httpClient;

    public RtsAddressSource(
        TokenService tokenService,
        HttpClient httpClient)
    {
        _tokenService = tokenService;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<RtsAddress>> SearchByPostcode(
        string postcode,
        CancellationToken cancellationToken)
    {
        var accessToken = await _tokenService.GetAccessTokenAsync(cancellationToken);

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                accessToken);

        var response = await _httpClient.GetAsync(
            $"api/v2/Rts/GetOrganisationList?postcode={Uri.EscapeDataString(postcode)}",
            cancellationToken);

        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<RtsResponse>(
            cancellationToken: cancellationToken);

        return result?.Result.RtsOrganisations
               ?? Enumerable.Empty<RtsAddress>();
    }

    public async Task<RtsAddress?> GetById(
        string rtsAddressId,
        CancellationToken cancellationToken)
    {
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

        return result?.Result.RtsOrganisations.Single();
    }
}