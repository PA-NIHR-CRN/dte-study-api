namespace NIHR.NotificationService.Enums;

public enum NotificationDeliveryStatus
{
    Accepted,
    Cancelled,
    PendingVirusCheck,
    VirusScanFailed,
    ValidationFailed,
    Created,
    Sending,
    Pending,
    Sent,
    Received,
    Delivered,
    TemporaryFailure,
    PermanentFailure,
    TechnicalFailure
}