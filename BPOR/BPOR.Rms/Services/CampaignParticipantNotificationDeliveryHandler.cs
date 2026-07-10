using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using Microsoft.EntityFrameworkCore;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;

namespace BPOR.Rms.Services;

public class CampaignParticipantNotificationDeliveryHandler(
    ParticipantDbContext context,
    IRefDataService refDataService,
    ILogger<CampaignParticipantNotificationDeliveryHandler> logger) 
    : INotificationDeliveryHandler<CampaignParticipantNotificationDeliveryHandler>
{
    public async Task HandleStatusChanged(string reference, NotificationDeliveryStatus currentStatus,
        CancellationToken cancellationToken)
    {
        if (!int.TryParse(reference, out var campaignParticipantId))
        {
            logger.LogError("Invalid callback reference {reference}", reference);
            throw new ArgumentException("Invalid reference", nameof(reference));
        }

        var participantEmail = await context.CampaignParticipant
            .FirstOrDefaultAsync(x => x.Id == campaignParticipantId, cancellationToken);

        if (participantEmail == null)
        {
            logger.LogError("CampaignParticipant not found for Id {campaignParticipantId}.", campaignParticipantId);
            throw new ArgumentException("Reference not found", nameof(reference));
        }
        
        switch (currentStatus)
        {
            case NotificationDeliveryStatus.Delivered:
                participantEmail.DeliveredAt = DateTime.UtcNow;
                participantEmail.DeliveryStatusId = refDataService.GetDeliveryStatusId(DeliveryStatusNames.Delivered);
                break;
            case NotificationDeliveryStatus.TemporaryFailure:
            case NotificationDeliveryStatus.PermanentFailure:
            case NotificationDeliveryStatus.TechnicalFailure:
                participantEmail.DeliveryStatusId = refDataService.GetDeliveryStatusId(DeliveryStatusNames.Failed);
                break;
            // This is a refactor - Existing behaviour was to do nothing for other statuses
        }
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public static string Key => "CMP";
}