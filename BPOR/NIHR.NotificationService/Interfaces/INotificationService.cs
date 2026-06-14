using NIHR.NotificationService.Controllers;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationService<THandler>
    where THandler : class, INotificationDeliveryHandler<THandler>
{
    Task SendEmail(string reference, Dictionary<string, string> personalisation, string templateId, string emailAddress,
        CancellationToken cancellationToken);

}

public interface INotificationService
{
    Task SendPreviewEmailAsync(SendNotificationRequest request, CancellationToken cancellationToken);
    
    Task<TemplateList> GetTemplatesAsync(CancellationToken cancellationToken);
    
    Task SendNotificationAsync(SendNotificationRequest notification, CancellationToken cancellationToken);

    Task ProcessDeliveryCallback(NotifyCallbackMessage message,
        CancellationToken cancellationToken);
}
