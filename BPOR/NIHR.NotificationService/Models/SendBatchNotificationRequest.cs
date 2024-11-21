using BPOR.Domain.Enums;

namespace NIHR.NotificationService.Models;

public class SendBatchNotificationRequest
{
    public ContactMethods ContactMethod { get; set; }

    // Common fields for all batch notifications
    public Dictionary<string, Dictionary<string, string>> PersonalisationData { get; set; }
    public Guid? TemplateId { get; set; }

    // Email fields
    public IEnumerable<string>? EmailAddresses { get; set; }

    // Letter fields
    public IEnumerable<string>? Addresses { get; set; }
}
