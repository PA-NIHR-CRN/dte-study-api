namespace BPOR.Rms;

public interface IRmsTaskQueue
{
    ValueTask QueueBackgroundWorkItemAsync(int id, CancellationToken cancellationToken);
    ValueTask<int> DequeueAsync(CancellationToken cancellationToken);
}
