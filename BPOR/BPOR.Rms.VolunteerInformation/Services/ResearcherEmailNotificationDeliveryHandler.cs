using BPOR.Domain.Entities;
using BPOR.Domain.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;
using DeliveryStatus = BPOR.Domain.Enums.DeliveryStatus;

namespace BPOR.Rms.VolunteerInformation;

public class ResearcherEmailNotificationDeliveryHandler(ParticipantDbContext db) 
    : INotificationDeliveryHandler<ResearcherEmailNotificationDeliveryHandler>
{
    public async Task HandleStatusChanged(string reference, NotificationDeliveryStatus currentStatus,
        CancellationToken cancellationToken)
    {
        if (!int.TryParse(reference, out var id))
        {
            throw new ArgumentException(nameof(reference));
        }

        var email = await db.StudyResearcherEmails.SingleOrDefaultAsync(r => r.Id == id, cancellationToken: cancellationToken);

        if (email != null)
        {
            email.DeliveryStatusId = currentStatus switch
            {
                NotificationDeliveryStatus.Delivered => (int) DeliveryStatus.Delivered,
                NotificationDeliveryStatus.Cancelled or
                NotificationDeliveryStatus.VirusScanFailed or
                NotificationDeliveryStatus.ValidationFailed or
                NotificationDeliveryStatus.TemporaryFailure or
                NotificationDeliveryStatus.PermanentFailure or
                NotificationDeliveryStatus.TechnicalFailure => (int) DeliveryStatus.Failed,
                
                _ => throw new ArgumentOutOfRangeException(nameof(currentStatus), currentStatus, null)
            };
            await db.SaveChangesAsync(cancellationToken);
        }
    }

    public static string Key { get; } = "RSR";
}