
using NIHR.NotificationService.Enums;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationDeliveryHandler
{
     Task HandleStatusChanged(string reference, NotificationDeliveryStatus currentStatus,
         CancellationToken cancellationToken);   
}

public interface INotificationDeliveryHandler<T> : INotificationDeliveryHandler
    where T : INotificationDeliveryHandler<T>
{
    static abstract string Key { get; }


}