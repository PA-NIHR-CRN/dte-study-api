using NIHR.NotificationService.Context;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace BPOR.Rms.Startup
{
    public class NullNotificationService : INotificationService
    {
        public Task SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task SendPreviewEmailAsync(SendEmailRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public async Task<EmailNotificationResponse> SendBatchEmailAsync(List<Notification> notifications, CancellationToken cancellationToken)
        {
            return new EmailNotificationResponse();
        }

        public Task<TemplateList> GetTemplatesAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new TemplateList());
        }
    }
}
