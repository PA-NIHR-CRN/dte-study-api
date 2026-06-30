namespace NIHR.NotificationService.Models;

public class UnkeyedSendNotificationRequest: SendNotificationRequestBase
{
    public string Reference { get; set; }
}