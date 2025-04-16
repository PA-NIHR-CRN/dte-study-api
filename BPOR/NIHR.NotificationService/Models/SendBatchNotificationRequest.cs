using BPOR.Domain.Enums;

namespace NIHR.NotificationService.Models;

public class SendBatchNotificationRequest
{
    public ContactMethodId ContactMethod { get; set; }

    // Common fields for all batch notifications
    public Dictionary<string, Dictionary<string, string>> PersonalisationData { get; set; } = new Dictionary<string, Dictionary<string, string>>();
    public Guid? TemplateId { get; set; }

    // Email fields
    public IEnumerable<string> EmailAddresses { get; set; } = new List<string>();

    // Letter fields
    public IEnumerable<string> Addresses { get; set; } = new List<string>();
}
