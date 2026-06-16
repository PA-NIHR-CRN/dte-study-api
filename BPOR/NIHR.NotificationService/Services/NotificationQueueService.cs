using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NIHR.NotificationService.Entities;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Services;

public class NotificationQueueService(INotificationService downstreamService,
    ILogger<NotificationQueueService> logger,
    Lazy<NotificationDbContext> db) : INotificationQueueService
{
    public Task<TemplateList> GetTemplates(CancellationToken cancellationToken) 
        => downstreamService.GetTemplates(cancellationToken);

    public Task SendNotification(SendNotificationRequest notification, CancellationToken cancellationToken)
        => SendNotifications([notification], cancellationToken);

    public Task ProcessDeliveryCallback(NotifyCallbackMessage message, CancellationToken cancellationToken)
        => downstreamService.ProcessDeliveryCallback(message, cancellationToken);

    public async Task SendNotifications(IEnumerable<SendNotificationRequest> notifications, CancellationToken cancellationToken)
    {
        foreach (var request in notifications)
        {
            request.Validate();
            Notification dbNotification = new()
            {
                Reference = request.Reference.ToString(),
                IsProcessed = false,
                NotificationDatas = new List<NotificationData>(request.Personalisation.Select(i =>
                    new NotificationData { Key = i.Key, Value = i.Value }))
            };
            dbNotification.NotificationDatas.Add(new(){ Key = PersonalisationKeys.TemplateId, Value = request.TemplateId });
            dbNotification.NotificationDatas.Add(new(){ Key = PersonalisationKeys.ContactMethod, Value = request.ContactMethod.ToString() });
            db.Value.Notifications.Add(dbNotification);
        }
        await db.Value.SaveChangesAsync(cancellationToken);
    }

    public async Task ProcessBatch(int batchSize, CancellationToken cancellationToken)
    {
        var notifications = await db.Value.Notifications
            .Where(n => !n.IsProcessed)
            .OrderBy(n => n.Id)
            .Take(1000).Include(n => n.NotificationDatas)
            .ToListAsync(cancellationToken);

        if (notifications.Count > 0)
        {
            logger.LogInformation("Processing {NotificationsCount} notifications", notifications.Count);
            
            try
            {
                foreach (var notification in notifications)
                {
                    var sendNotificationRequest = CreateSendNotificationRequest(notification);
                    await downstreamService.SendNotification(sendNotificationRequest, cancellationToken);
                    notification.IsProcessed = true;
                    await db.Value.SaveChangesAsync(cancellationToken); // Persist IsProcessed on a per-record basis
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while sending notification emails");
            }
        }
    }

    private static SendNotificationRequest CreateSendNotificationRequest(Notification notification)
    {
        var personalisation = notification.NotificationDatas.ToDictionary(x => x.Key, x => x.Value);
        var result = new SendNotificationRequest()
        {
            Reference = NotificationReference.Parse(notification.Reference),
            Personalisation = personalisation,
            ContactMethod =
                Enum.Parse<GovUkNotifyContactMethod>(personalisation[PersonalisationKeys.ContactMethod]),
            TemplateId = personalisation[PersonalisationKeys.TemplateId]
        };

        result.Validate();
        return result;
    }
}