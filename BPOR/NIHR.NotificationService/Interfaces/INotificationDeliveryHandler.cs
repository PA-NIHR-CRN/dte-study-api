
using NIHR.NotificationService.Enums;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationDeliveryHandler
{
     Task HandleStatusChanged(string reference, NotificationDeliveryStatus currentStatus,
         CancellationToken cancellationToken);   
}