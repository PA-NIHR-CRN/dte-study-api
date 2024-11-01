namespace NIHR.NotificationService.Models;

public class SendBatchNotificationRequest
{
    public ContactMethod ContactMethod { get; set; }

    // Common fields for all batch notifications
    public Dictionary<string, Dictionary<string, string>> PersonalisationData { get; set; }

    // Email fields
    public IEnumerable<string>? EmailAddresses { get; set; }
    public Guid? EmailTemplateId { get; set; }

    // Letter fields
    public IEnumerable<string>? Addresses { get; set; }
    public Guid? LetterTemplateId { get; set; }
}
