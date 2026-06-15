using NIHR.NotificationService.Controllers;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationService<THandler>
    where THandler : class, INotificationDeliveryHandler<THandler>
{
    Task SendEmail(string reference, Dictionary<string, string> personalisation, string templateId, string emailAddress,
        CancellationToken cancellationToken);
    
    Task SendNotifications(IEnumerable<UnkeyedSendNotificationRequest> notifications, CancellationToken cancellationToken);
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
