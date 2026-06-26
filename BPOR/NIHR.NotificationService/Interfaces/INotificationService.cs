using NIHR.NotificationService.Controllers;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Interfaces;

public interface INotificationService<THandler>
    where THandler : class, INotificationDeliveryHandler<THandler>
{
    Task SendNotifications(IEnumerable<UnkeyedSendNotificationRequest> notifications,
        CancellationToken cancellationToken);

    Task SendNotification(UnkeyedSendNotificationRequest notification, CancellationToken cancellationToken)
        => SendNotifications([notification], cancellationToken);
}

public interface INotificationQueueService
{
    Task ProcessBatch(int batchSize, CancellationToken cancellationToken);
}

public interface INotificationService
{
    Task<TemplateList> GetTemplates(CancellationToken cancellationToken);

    Task SendNotifications(IEnumerable<SendNotificationRequest> notifications, CancellationToken cancellationToken);

    Task SendNotification(SendNotificationRequest notification, CancellationToken cancellationToken)
        => SendNotifications([notification], cancellationToken);

    Task ProcessDeliveryCallback(NotifyCallbackMessage message,
        CancellationToken cancellationToken);
}

public interface IDownstreamNotificationService
{
    Task<TemplateList> GetTemplates(CancellationToken cancellationToken);

    Task<SendNotificationResult> SendNotification(SendNotificationRequest notification,
        CancellationToken cancellationToken);
}

public record SendNotificationResult(
    SendNotificationStatus Status,
    string? ErrorMessage,
    NotificationDeliveryStatus? DeliveryStatus)
{
    public static SendNotificationResult TemporaryFailure(string errorMessage) 
        => new(SendNotificationStatus.TemporaryFailure, errorMessage, null);

    public static SendNotificationResult Success(NotificationDeliveryStatus deliveryStatus) 
        => new(SendNotificationStatus.Success, null, deliveryStatus);

    public static SendNotificationResult PermanentFailure(string errorMessage) 
        => new(SendNotificationStatus.PermanentFailure, errorMessage, null);
}

public enum SendNotificationStatus
{
    Success,
    TemporaryFailure,
    PermanentFailure
}