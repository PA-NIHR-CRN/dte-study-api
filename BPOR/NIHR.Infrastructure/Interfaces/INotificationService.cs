using System.Threading;
using System.Threading.Tasks;
using NIHR.Infrastructure.Models;

namespace NIHR.Infrastructure.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken);
        Task SendBatchEmailAsync(SendBatchEmailRequest request, CancellationToken cancellationToken);
    }
}
