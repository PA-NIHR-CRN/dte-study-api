using NIHR.NotificationService.Enums;

namespace NIHR.NotificationService.Entities;

public class Notification
{
    public int Id { get; set; }
    public string Reference { get; set; }
    public ICollection<NotificationData> NotificationDatas { get; set; } = new List<NotificationData>();
    public bool IsProcessed  { get; set; }
    public GovUkNotifyContactMethod ContactMethod { get; set; }
}
