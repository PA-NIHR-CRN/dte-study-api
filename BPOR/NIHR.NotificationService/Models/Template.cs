using NIHR.NotificationService.Enums;

namespace NIHR.NotificationService.Interfaces;

public class Template
{
    public string Id {get; set;}
    public string Name { get; set; }
    public GovUkNotifyContactMethod ContactMethod { get; set; }
}