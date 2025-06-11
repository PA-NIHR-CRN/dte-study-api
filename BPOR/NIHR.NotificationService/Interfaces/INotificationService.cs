using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationService
{
    Task SendPreviewEmailAsync(SendNotificationRequest request, CancellationToken cancellationToken);
    Task<TemplateList> GetTemplatesAsync(CancellationToken cancellationToken);
    Task ProcessNextNotificationBatchAsync(CancellationToken stoppingToken);
}
