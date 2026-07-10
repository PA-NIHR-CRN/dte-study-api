namespace NIHR.NotificationService.Interfaces;

public interface INotificationQueueService
{
    Task ProcessBatch(int batchSize, CancellationToken cancellationToken);
}