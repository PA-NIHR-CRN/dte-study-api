using System.Text.Json;
using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.Abstractions.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace BPOR.Rms.ApiClient;

internal class RmsApiClient(HttpClient httpClient, ILogger<RmsApiClient> logger) : IRmsApiClient
{
    public async Task TrackEvent(string token, CampaignParticipantEventType eventType,  CancellationToken cancellationToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["eventType"] = eventType.ToString()
        };

        var link = QueryHelpers.AddQueryString("volunteer/trackevent/" + token, queryParams);
        
        using HttpResponseMessage response = await httpClient.GetAsync(link, cancellationToken);
        
        EnsureSuccess(response);
    }
    
    public async Task<GetInformationResponse> GetInformation(string token, CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await httpClient.GetAsync("volunteer/information/" + token, cancellationToken);

        EnsureSuccess(response);
    
        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<GetInformationResponse>(jsonResponse,
            new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
    }
    
    public async Task<GetVolunteerInformationPageResponse> GetVolunteerInformationPage(string token, CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await httpClient.GetAsync("volunteer/informationpage/" + token, cancellationToken);

        EnsureSuccess(response);
    
        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<GetVolunteerInformationPageResponse>(jsonResponse,
            new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
    }

    private void EnsureSuccess(HttpResponseMessage response)
    {
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception caught)
        {
            logger.LogError(caught, $"Error requesting {response.RequestMessage.RequestUri}");
            throw;
        }
    }
}