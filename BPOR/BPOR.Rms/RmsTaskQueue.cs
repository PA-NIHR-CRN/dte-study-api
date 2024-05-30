using System.Threading.Channels;
using NIHR.Infrastructure;

namespace BPOR.Rms;

public class RmsTaskQueue : IRmsTaskQueue
{
    private readonly ILogger _logger;
    private readonly Channel<Func<CancellationToken, ValueTask>> _queue;

    public RmsTaskQueue(int capacity, ILogger logger)
    {
        _logger = logger;
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _queue = Channel.CreateBounded<Func<CancellationToken, ValueTask>>(options);
    }

    public async ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem)
    {
        _logger.LogInformation("Queueing background work item");
        ArgumentNullException.ThrowIfNull(workItem);
        await _queue.Writer.WriteAsync(workItem);
    }

    public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Dequeuing background work item");
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}
