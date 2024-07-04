using BPOR.Rms.Models.Email;

namespace BPOR.Rms;

public interface IRmsTaskQueue
{
    ValueTask QueueBackgroundWorkItemAsync(int id, string callback, CancellationToken cancellationToken);
    ValueTask<EmailServiceQueueItem> DequeueAsync(CancellationToken cancellationToken);
}
