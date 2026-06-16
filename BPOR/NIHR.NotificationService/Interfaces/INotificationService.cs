using NIHR.NotificationService.Controllers;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationService<THandler>
    where THandler : class, INotificationDeliveryHandler<THandler>
{
    Task SendNotifications(IEnumerable<UnkeyedSendNotificationRequest> notifications, CancellationToken cancellationToken);
    
    Task SendNotification(UnkeyedSendNotificationRequest notification, CancellationToken cancellationToken)
        => SendNotifications([notification], cancellationToken);
}

public interface INotificationQueueService
{
    Task ProcessBatch(int batchSize, CancellationToken cancellationToken);
}

public interface INotificationService
{
    Task<TemplateList> GetTemplates(CancellationToken cancellationToken);
    
    Task SendNotifications(IEnumerable<SendNotificationRequest> notifications, CancellationToken cancellationToken);
    
    Task SendNotification(SendNotificationRequest notification, CancellationToken cancellationToken)
        => SendNotifications([notification], cancellationToken);

    Task ProcessDeliveryCallback(NotifyCallbackMessage message,
        CancellationToken cancellationToken);
}

public interface IDownstreamNotificationService
{
    Task<TemplateList> GetTemplates(CancellationToken cancellationToken);

    Task<NotificationDeliveryStatus> SendNotification(SendNotificationRequest notification, CancellationToken cancellationToken);
}
