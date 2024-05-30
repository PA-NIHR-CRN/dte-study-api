namespace NIHR.NotificationService.Models;

public class SendEmailRequest
{
    public string EmailAddress { get; set; }
    public Guid EmailTemplateId { get; set; }
    public Dictionary<string, dynamic> Personalisation { get; set; }
    public string Reference { get; set; }
}
