using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NIHR.Infrastructure;

public class HostedNotificationQueueService : BackgroundService
{
    private readonly ILogger<HostedNotificationQueueService> _logger;
    private readonly INotificationTaskQueue _taskQueue;

    public HostedNotificationQueueService(INotificationTaskQueue taskQueue, ILogger<HostedNotificationQueueService> logger)
    {
        _taskQueue = taskQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Queued Hosted Service is running");

        await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Queued Hosted Service is working");
            var workItem = await _taskQueue.DequeueAsync(stoppingToken);

            try
            {
                await workItem(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing {WorkItem}", nameof(workItem));
            }
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Queued Hosted Service is stopping");
        await base.StopAsync(stoppingToken);
    }
}
