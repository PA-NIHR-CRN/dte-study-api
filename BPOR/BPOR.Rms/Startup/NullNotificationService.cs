using NIHR.NotificationService.Context;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace BPOR.Rms.Startup
{
    public class NullNotificationService : INotificationService
    {
        public Task SendEmailAsync(SendNotificationRequest request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task SendPreviewEmailAsync(SendNotificationRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public async Task<NotificationResponse> SendBatchNotificationAsync(List<Notification> notifications, CancellationToken cancellationToken)
        {
            return new NotificationResponse();
        }

        public Task<TemplateList> GetTemplatesAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new TemplateList());
        }
    }
}
