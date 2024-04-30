using NIHR.Infrastructure.Interfaces;
using NIHR.Infrastructure.Models;

namespace BPOR.Rms.Startup
{
    public class NullNotificationService : INotificationService
    {
        public Task SendBatchEmailAsync(SendBatchEmailRequest request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}