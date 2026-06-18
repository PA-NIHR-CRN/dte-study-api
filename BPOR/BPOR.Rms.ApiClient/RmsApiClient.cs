using System.Text.Json;
using BPOR.Rms.Abstractions.Models;

namespace BPOR.Rms.ApiClient;

internal class RmsApiClient(HttpClient httpClient) : IRmsApiClient
{
    public async Task<GetInformationResponse> GetInformation(string token, CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await httpClient.GetAsync("volunteer/information/" + token, cancellationToken);

        response.EnsureSuccessStatusCode();
    
        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<GetInformationResponse>(jsonResponse,
            new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
    }
    
    public async Task<GetVolunteerInformationPageResponse> GetVolunteerInformationPage(string token)
    {
        using HttpResponseMessage response = await httpClient.GetAsync("volunteer/informationpage/" + token);

        response.EnsureSuccessStatusCode();
    
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GetVolunteerInformationPageResponse>(jsonResponse,
            new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
    }
}