
using NIHR.NotificationService.Enums;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationStatusSink
{
    Task HandleStatusChanged(string reference, NotificationDeliveryStatus currentStatus,
        CancellationToken cancellationToken);
}