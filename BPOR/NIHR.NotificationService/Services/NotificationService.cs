using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NIHR.NotificationService.Entities;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Services;

public class NotificationService(IDownstreamNotificationService downstreamService,
    ILogger<NotificationService> logger,
    NotificationDbContext db,
    IReadOnlyDictionary<string, INotificationDeliveryHandler> notificationStatusSinks) 
    : INotificationQueueService, INotificationService
{
    public Task<TemplateList> GetTemplates(CancellationToken cancellationToken) 
        => downstreamService.GetTemplates(cancellationToken);

    public Task SendNotification(SendNotificationRequest notification, CancellationToken cancellationToken)
        => SendNotifications([notification], cancellationToken);

    public async Task SendNotifications(IEnumerable<SendNotificationRequest> notifications, CancellationToken cancellationToken)
    {
        foreach (var request in notifications)
        {
            request.Validate();

            request.Personalisation[PersonalisationKeys.UniqueReference] = request.Reference.ToString();
            request.Personalisation[PersonalisationKeys.TemplateId] = request.TemplateId;
            request.Personalisation[PersonalisationKeys.ContactMethod] = request.ContactMethod.ToString();

            
            Notification dbNotification = new()
            {
                IsProcessed = false,
                NotificationDatas = new List<NotificationData>(request.Personalisation.Select(i =>
                    new NotificationData { Key = i.Key, Value = i.Value }))
            };
            db.Notifications.Add(dbNotification);
        }
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task ProcessBatch(int batchSize, CancellationToken cancellationToken)
    {
        var notifications = await db.Notifications
            .Where(n => !n.IsProcessed)
            .OrderBy(n => n.Id)
            .Take(batchSize).Include(n => n.NotificationDatas)
            .ToListAsync(cancellationToken);

        if (notifications.Count > 0)
        {
            logger.LogInformation("Processing {NotificationsCount} notifications", notifications.Count);
            
            try
            {
                foreach (var notification in notifications)
                {
                    var sendNotificationRequest = CreateSendNotificationRequest(notification);
                    var result = await downstreamService.SendNotification(sendNotificationRequest, cancellationToken);
                    switch (result.Status)
                    {
                        case SendNotificationStatus.Success:
                            notification.IsProcessed = true;
                            await db.SaveChangesAsync(cancellationToken);
                            await ProcessDeliveryCallback(sendNotificationRequest.Reference, result.DeliveryStatus!.Value, cancellationToken);
                            break;
                        case SendNotificationStatus.TemporaryFailure:
                            // Implement back pressure mechanism and/or limit retries. For now
                            // abandon the rest of the batch so that the hosted service retries
                            // after a short interval.
                            return;
                        case SendNotificationStatus.PermanentFailure:
                            notification.IsProcessed = true;
                            await db.SaveChangesAsync(cancellationToken);
                            await ProcessDeliveryCallback(sendNotificationRequest.Reference, NotificationDeliveryStatus.PermanentFailure, cancellationToken);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
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
            Reference = NotificationReference.Parse(personalisation[PersonalisationKeys.UniqueReference]),
            Personalisation = personalisation,
            ContactMethod = Enum.Parse<GovUkNotifyContactMethod>(personalisation[PersonalisationKeys.ContactMethod]),
            TemplateId = personalisation[PersonalisationKeys.TemplateId]
        };

        result.Validate();
        return result;
    }
    
    public async Task ProcessDeliveryCallback(NotifyCallbackMessage message,
        CancellationToken cancellationToken)
    {
        NotificationReference notificationReference;

        if (int.TryParse(message.Reference, out _))
        {
            // This clause is to support the transition to the new notification reference scheme
            notificationReference = new NotificationReference("CMP", message.Reference);
        }
        else if (!NotificationReference.TryParse(message.Reference, out notificationReference!)) // This is the new reference format: "{upstream-key}:{upstream-ref}"
        {
            throw new Exception("Invalid reference");
        }
            
        var status = message.Status switch
        {
            "delivered" => NotificationDeliveryStatus.Delivered,
            "temporary-failure" => NotificationDeliveryStatus.TemporaryFailure,
            "permanent-failure" => NotificationDeliveryStatus.PermanentFailure,
            "technical-failure" => NotificationDeliveryStatus.TechnicalFailure,
            _ => throw new ArgumentOutOfRangeException(nameof(message.Status), message.Status, null)
        };

        await ProcessDeliveryCallback(notificationReference, status, cancellationToken);
    }

    private async Task ProcessDeliveryCallback(NotificationReference reference,
        NotificationDeliveryStatus status, CancellationToken cancellationToken)
    {
        if (!notificationStatusSinks.TryGetValue(reference.UpstreamProviderKey, out var upstreamSink))
        {
            throw new Exception("Upstream sink not found");
        }

        await upstreamSink.HandleStatusChanged(reference.UpstreamReference, status, cancellationToken);
    }
}