using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

namespace BPOR.Rms.Startup
{
    public class NullNotificationService : INotificationService
    {
        public Task<IEnumerable<Template>> GetTemplates(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SendNotifications(IEnumerable<SendNotificationRequest> notifications, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SendNotification(SendNotificationRequest notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ProcessDeliveryCallback(NotifyCallbackMessage message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
