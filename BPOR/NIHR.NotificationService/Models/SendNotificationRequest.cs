namespace NIHR.NotificationService.Models;

public class SendNotificationRequest : SendNotificationRequestBase
{
    public NotificationReference Reference { get; set; }
}