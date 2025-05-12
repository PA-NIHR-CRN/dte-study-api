using System.Threading.Channels;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Services;

namespace BPOR.Rms;

public class RmsTaskQueue : IRmsTaskQueue
{
    private readonly ILogger _logger;
    private readonly Channel<CampaignServiceQueueItem> _queue;

    public RmsTaskQueue(int capacity, ILogger logger)
    {
        _logger = logger;
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _queue = Channel.CreateBounded<CampaignServiceQueueItem>(options);
    }
    

    public async ValueTask QueueBackgroundWorkItemAsync(int id, string callback, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Queueing background work item");
        ArgumentNullException.ThrowIfNull(id);
        var item = new CampaignServiceQueueItem { Id = id, Callback = callback };
        await _queue.Writer.WriteAsync(item, cancellationToken);
    }

    async ValueTask<CampaignServiceQueueItem> IRmsTaskQueue.DequeueAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Dequeuing background work item");
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}

