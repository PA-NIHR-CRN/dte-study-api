using System.Threading.Channels;
using BPOR.Rms.Services;

namespace BPOR.Rms;

public class RmsTaskQueue : IRmsTaskQueue
{
    private readonly ILogger _logger;
    private readonly Channel<int> _queue;

    public RmsTaskQueue(int capacity, ILogger logger)
    {
        _logger = logger;
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _queue = Channel.CreateBounded<int>(options);
    }
    

    public async ValueTask QueueBackgroundWorkItemAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Queueing background work item");
        ArgumentNullException.ThrowIfNull(id);
        await _queue.Writer.WriteAsync(id, cancellationToken);
    }

    async ValueTask<int> IRmsTaskQueue.DequeueAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Dequeuing background work item");
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}
