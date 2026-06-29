using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

namespace BPOR.Rms.Startup
{
    public class NullNotificationService : INotificationService
    {
        public Task<IEnumerable<Template>> GetTemplates(CancellationToken cancellationToken)
        {
            return Task.FromResult(Array.Empty<Template>().AsEnumerable());
        }

        public Task SendNotifications(IEnumerable<SendNotificationRequest> notifications, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SendNotification(SendNotificationRequest notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task ProcessDeliveryCallback(NotifyCallbackMessage message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
