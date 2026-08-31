using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace NIHR.Quartz;

public static class DiExtensions
{
    public static void AddNihrQuartz(
        this IServiceCollection services,
        Func<IServiceProvider, IScheduler, Task> scheduleJobs)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options =>
        {
            // when shutting down we want jobs to complete gracefully
            options.WaitForJobsToComplete = true;
        });
        services.AddHostedService<SchedulerFactoryService>(i => new SchedulerFactoryService(i, i.GetRequiredService<ISchedulerFactory>(), scheduleJobs));
    }
}