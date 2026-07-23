using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.Abstractions.Models;

namespace BPOR.Rms.ApiClient;

public interface IRmsApiClient
{
    Task<GetInformationResponse> GetInformation(string token, CancellationToken cancellationToken);
    Task<GetVolunteerInformationPageResponse> GetVolunteerInformationPage(string token, CancellationToken cancellationToken);
    Task TrackEvent(string token, CampaignParticipantEventType eventType,  CancellationToken cancellationToken);
}