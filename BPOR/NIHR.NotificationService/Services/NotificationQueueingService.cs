using NIHR.NotificationService.Entities;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Services;

public class NotificationQueueingService(INotificationService downstreamService,
    Lazy<NotificationDbContext> db) : INotificationService
{
    public Task<TemplateList> GetTemplates(CancellationToken cancellationToken) 
        => downstreamService.GetTemplates(cancellationToken);

    public Task SendNotification(SendNotificationRequest notification, CancellationToken cancellationToken)
        => SendNotifications([notification], cancellationToken);

    public Task ProcessDeliveryCallback(NotifyCallbackMessage message, CancellationToken cancellationToken)
        => downstreamService.ProcessDeliveryCallback(message, cancellationToken);

    public Task SendNotifications(IEnumerable<SendNotificationRequest> notifications, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}