using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NIHR.NotificationService.Interfaces;

namespace NIHR.NotificationService.Services
{
    public class HostedNotificationQueueService(
        ILogger<HostedNotificationQueueService> logger,
        INotificationQueueService notificationQueueService)
        : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Hosted Notification Queue Service is running");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await notificationQueueService.ProcessBatch(1000, stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while processing notifications");
                }

                // Wait for a certain period before polling again
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Hosted Notification Queue Service is stopping");
            await base.StopAsync(stoppingToken);
        }
    }
}