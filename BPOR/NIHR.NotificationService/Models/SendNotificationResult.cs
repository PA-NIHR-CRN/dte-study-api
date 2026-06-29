using NIHR.NotificationService.Enums;

namespace NIHR.NotificationService.Interfaces;

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