using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NIHR.NotificationService.Context;
using NIHR.NotificationService.Interfaces;
using BPOR.Domain.Enums;

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
                    await ProcessNotificationsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing notifications");
                }

                // Wait for a certain period before polling again
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private async Task ProcessNotificationsAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
            var participantDbContext = scope.ServiceProvider.GetRequiredService<ParticipantDbContext>();

            var notifications = await context.Notifications
                .Where(n => !n.IsProcessed)
                .OrderBy(n => n.Id)
                .Take(1000).Include(n => n.NotificationDatas)
                .ToListAsync(stoppingToken);

            if (notifications.Count > 0)
            {
                _logger.LogInformation("Processing {NotificationsCount} notifications", notifications.Count);

                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                try
                {
                    await notificationService.SendBatchNotificationAsync(notifications, stoppingToken);

                    foreach (var notification in notifications)
                    {
                        await UpdateDeliveryStatusForLetterAsync(notification, participantDbContext);

                        // TODO are we marking as processed or deleting the notification?
                        notification.IsProcessed = true;
                        
                    }

                    await context.SaveChangesAsync(stoppingToken);
                    await participantDbContext.SaveChangesAsync(stoppingToken);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while sending notification emails");
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Hosted Notification Queue Service is stopping");
            await base.StopAsync(stoppingToken);
        }

        private async Task UpdateDeliveryStatusForLetterAsync(Notification notification, ParticipantDbContext context)
        {
            var personalisation = notification.NotificationDatas.ToDictionary(x => x.Key, x => x.Value);

            if (personalisation.TryGetValue("campaignTypeId", out var ctValue)
                && int.TryParse(ctValue, out var campaignType)
                && campaignType == (int)ContactMethodId.Letter)
            {
                if (!personalisation.TryGetValue("campaignParticipantId", out var campaignParticipantIdValue)
                    || !int.TryParse(campaignParticipantIdValue, out var campaignParticipantId))
                {
                    throw new KeyNotFoundException("campaignParticipantId not found or invalid.");
                }

                var participant = await context.CampaignParticipant.FirstOrDefaultAsync(x => x.Id == campaignParticipantId);
                if (participant != null)
                {
                    participant.DeliveredAt = DateTime.UtcNow;
                    participant.DeliveryStatusId = (int)DeliveryStatus.Delivered;
                }
            }
        }
    }
}