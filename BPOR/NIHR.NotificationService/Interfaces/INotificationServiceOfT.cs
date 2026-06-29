using NIHR.NotificationService.Models;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationService<THandler>
    where THandler : class, INotificationDeliveryHandler<THandler>
{
    Task SendNotifications(IEnumerable<UnkeyedSendNotificationRequest> notifications,
        CancellationToken cancellationToken);

    Task SendNotification(UnkeyedSendNotificationRequest notification, CancellationToken cancellationToken)
        => SendNotifications([notification], cancellationToken);
}