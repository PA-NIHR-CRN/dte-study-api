using NIHR.NotificationService.Models;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<Template>> GetTemplates(CancellationToken cancellationToken);

    Task SendNotifications(IEnumerable<SendNotificationRequest> notifications, CancellationToken cancellationToken);

    Task SendNotification(SendNotificationRequest notification, CancellationToken cancellationToken)
        => SendNotifications([notification], cancellationToken);

    Task ProcessDeliveryCallback(NotifyCallbackMessage message,
        CancellationToken cancellationToken);
}