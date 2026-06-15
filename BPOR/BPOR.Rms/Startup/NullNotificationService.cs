using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace BPOR.Rms.Startup
{
    public class NullNotificationService : INotificationService
    {
        public Task<TemplateList> GetTemplates(CancellationToken cancellationToken)
        {
            return Task.FromResult(new TemplateList());
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
