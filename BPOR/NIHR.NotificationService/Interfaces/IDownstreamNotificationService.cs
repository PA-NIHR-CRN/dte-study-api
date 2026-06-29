using NIHR.NotificationService.Models;

namespace NIHR.NotificationService.Interfaces;

public interface IDownstreamNotificationService
{
    Task<IEnumerable<Template>> GetTemplates(CancellationToken cancellationToken);

    Task<SendNotificationResult> SendNotification(SendNotificationRequest notification,
        CancellationToken cancellationToken);
}