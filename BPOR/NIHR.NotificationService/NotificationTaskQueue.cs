using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using NIHR.Infrastructure;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

namespace NIHR.NotificationService;

public class NotificationTaskQueue : INotificationTaskQueue
{
    private readonly ILogger _logger;
    private readonly Channel<SendBatchEmailRequest> _queue;

    public NotificationTaskQueue(int capacity, ILogger logger)
    {
        _logger = logger;
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _queue = Channel.CreateBounded<SendBatchEmailRequest>(options);
    }

    public async ValueTask QueueBackgroundWorkItemAsync(SendBatchEmailRequest workItem, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Queueing background work item");
        ArgumentNullException.ThrowIfNull(workItem);
        await _queue.Writer.WriteAsync(workItem);
    }

    public async ValueTask<SendBatchEmailRequest> DequeueAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Dequeuing background work item");
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}
