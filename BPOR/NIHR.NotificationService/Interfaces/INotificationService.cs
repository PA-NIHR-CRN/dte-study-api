using NIHR.Infrastructure.Models;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationService
{
    Task SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken);

    Task<EmailNotificationResponse> SendBatchEmailAsync(SendBatchEmailRequest request,
        CancellationToken cancellationToken);

    Task<TemplateList> GetTemplatesAsync(CancellationToken cancellationToken);
}
