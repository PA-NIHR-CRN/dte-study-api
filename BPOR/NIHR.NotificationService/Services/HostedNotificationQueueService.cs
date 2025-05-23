using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NIHR.NotificationService.Context;
using NIHR.NotificationService.Interfaces;
using BPOR.Domain.Enums;
using BPOR.Rms.Constants;

namespace NIHR.NotificationService.Services
{
    public class HostedNotificationQueueService : BackgroundService
    {
        private readonly ILogger<HostedNotificationQueueService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public HostedNotificationQueueService(ILogger<HostedNotificationQueueService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Hosted Notification Queue Service is running");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                    await notificationService.ProcessNextNotificationBatchAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing notifications");
                }

                // Wait for a certain period before polling again
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }



        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Hosted Notification Queue Service is stopping");
            await base.StopAsync(stoppingToken);
        }
    }
}