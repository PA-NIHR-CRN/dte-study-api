using System;
using System.Threading;
using System.Threading.Tasks;

namespace NIHR.Infrastructure
{
    public interface IBackgroundTaskQueue
    {
        ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem);
        ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken);
    }

    public interface INotificationTaskQueue : IBackgroundTaskQueue
    {
    }

    public interface IRmsTaskQueue : IBackgroundTaskQueue
    {
    }
}
