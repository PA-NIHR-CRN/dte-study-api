using BPOR.Domain.Entities;
using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.VolunteerInformation.Data;

public interface IStudyRepository
{
    Task<Study?> GetStudy(int studyId, CancellationToken cancellationToken);
    
    Task<bool> UpdateStudy(int studyId, Action<Study> action, CancellationToken cancellationToken);
}

public interface ICampaignParticipantRepository
{
    Task<bool> TrackEvent(int campaignid, int participantId, CampaignParticipantEventType eventType,
        CancellationToken cancellationToken);
}