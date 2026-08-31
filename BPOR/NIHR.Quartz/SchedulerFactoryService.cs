using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace NIHR.Quartz;

public class SchedulerFactoryService(IServiceProvider serviceProvider, ISchedulerFactory schedulerFactory,
    Func<IServiceProvider, IScheduler, Task> scheduleJobs) : IHostedLifecycleService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task StartingAsync(CancellationToken cancellationToken)
    {
        var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

        await scheduleJobs(serviceProvider, scheduler);
    }

    public Task StartedAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}