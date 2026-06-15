using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NIHR.NotificationService.Entities;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

namespace NIHR.NotificationService.Services
{
    public class HostedNotificationQueueService(
        ILogger<HostedNotificationQueueService> logger,
        IServiceProvider serviceProvider)
        : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Hosted Notification Queue Service is running");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessNotificationsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while processing notifications");
                }

                // Wait for a certain period before polling again
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private async Task ProcessNotificationsAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();

            var notifications = await context.Notifications
                .Where(n => !n.IsProcessed)
                .OrderBy(n => n.Id)
                .Take(1000).Include(n => n.NotificationDatas)
                .ToListAsync(stoppingToken);

            if (notifications.Count > 0)
            {
                logger.LogInformation("Processing {NotificationsCount} notifications", notifications.Count);

                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                try
                {
                    foreach (var notification in notifications)
                    {
                        var personalisation = notification.NotificationDatas.ToDictionary(x => x.Key, x => x.Value);
                        var sendNotificationRequest = CreateSendNotificationRequest(personalisation);
                        await notificationService.SendNotification(sendNotificationRequest, stoppingToken);
                        notification.IsProcessed = true;
                        await context.SaveChangesAsync(stoppingToken); // Persist IsProcessed on a per-record basis
                    }


                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error occurred while sending notification emails");
                }
            }
        }

        private static SendNotificationRequest CreateSendNotificationRequest(Dictionary<string, string> personalisation)
        {
            if (!personalisation.TryGetValue(PersonalisationKeys.CampaignParticipantId, out var reference))
            {
                throw new KeyNotFoundException("campaignParticipantId not found in personalisation data.");
            }

            if (!personalisation.TryGetValue(PersonalisationKeys.TemplateId, out var templateId))
            {
                throw new KeyNotFoundException("templateId not found in personalisation data.");
            }

            if (!personalisation.TryGetValue(PersonalisationKeys.CampaignTypeId, out var campaignTypeIdStr))
            {
                throw new KeyNotFoundException("campaignTypeId not found in personalisation data.");
            }

            if (!int.TryParse(campaignTypeIdStr, out var campaignTypeId))
            {
                throw new ArgumentException($"campaignTypeId '{campaignTypeIdStr}' is not a valid integer.");
            }

            var request = new SendNotificationRequest
            {
                TemplateId = templateId,
                Personalisation = personalisation,
                Reference = new NotificationReference("CMP", reference),
                ContactMethod = (GovUkNotifyContactMethod)campaignTypeId
            };

            var contactMethod = (GovUkNotifyContactMethod)int.Parse(personalisation[PersonalisationKeys.CampaignTypeId]);

            switch (contactMethod)
            {
                case GovUkNotifyContactMethod.Email:
                    if (!request.Personalisation.TryGetValue("email", out var email) ||
                        string.IsNullOrWhiteSpace(email))
                    {
                        throw new ArgumentException(
                            "EmailAddress is required in personalisation for email notifications.");
                    }

                    request.EmailAddress = email;
                    break;

                case GovUkNotifyContactMethod.Letter:
                    if (!personalisation.TryGetValue("address_line_1", out var addressLine1) ||
                        !personalisation.TryGetValue("address_line_5", out var town) ||
                        !personalisation.TryGetValue("address_line_6", out var postcode))
                    {
                        throw new KeyNotFoundException("Letter notifications require at least 3 address lines");
                    }

                    request.Personalisation["address_line_1"] = addressLine1;

                    for (int i = 2; i <= 4; i++) // assign optional address fields
                    {
                        var key = $"address_line_{i}";
                        if (personalisation.TryGetValue(key, out var value))
                        {
                            request.Personalisation[key] = value;
                        }
                    }

                    request.Personalisation["address_line_5"] = town;
                    request.Personalisation["address_line_6"] = postcode;
                    request.Personalisation["address_postcode"] = postcode;
                    break;

                default:
                    throw new NotSupportedException($"Contact method {contactMethod} is not supported.");
            }

            return request;
        }


        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Hosted Notification Queue Service is stopping");
            await base.StopAsync(stoppingToken);
        }
    }
}