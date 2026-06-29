using BPOR.Domain.Entities;
using BPOR.Rms.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;

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

public class CampaignParticipantRepository(ParticipantDbContext db) : ICampaignParticipantRepository
{
    public async Task<bool> TrackEvent(int campaignid, int participantId, CampaignParticipantEventType eventType,
        CancellationToken cancellationToken)
    {
        var campaignParticipant = await db.CampaignParticipant.SingleOrDefaultAsync(i =>
            i.CampaignId == campaignid && i.ParticipantId == participantId, cancellationToken: cancellationToken);

        if (campaignParticipant == null)
        {
            return false;
        }
        
        switch (eventType)
        {
            case CampaignParticipantEventType.CampaignEmailLinkClick:
                campaignParticipant.VipEmailLinkClickedAtUtc = DateTime.UtcNow;
                break;
            case CampaignParticipantEventType.ExternalWebsiteLinkClick:
                campaignParticipant.VipExternalLinkClickedAtUtc = DateTime.UtcNow;
                break;
            case CampaignParticipantEventType.PrescreenerLinkClick:
                campaignParticipant.VipPrescreenerLinkClickedAtUtc = DateTime.UtcNow;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null);
        }
        
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}