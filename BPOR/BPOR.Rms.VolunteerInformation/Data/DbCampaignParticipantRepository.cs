using BPOR.Domain.Entities;
using BPOR.Rms.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.VolunteerInformation.Data;

public class DbCampaignParticipantRepository(ParticipantDbContext db) : ICampaignParticipantRepository
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