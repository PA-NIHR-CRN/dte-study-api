using NIHR.NotificationService.Models;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationTaskQueue
{
    ValueTask QueueBackgroundWorkItemAsync(SendBatchEmailRequest workItem, CancellationToken cancellationToken);
    ValueTask<SendBatchEmailRequest> DequeueAsync(CancellationToken cancellationToken);
}
