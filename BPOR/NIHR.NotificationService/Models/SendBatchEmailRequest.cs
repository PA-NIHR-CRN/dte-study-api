using System.Collections.Concurrent;

namespace NIHR.NotificationService.Models
{
    public class SendBatchEmailRequest
    {
        public IEnumerable<string> EmailAddresses { get; set; }
        public Dictionary<string, Dictionary<string, dynamic>> PersonalisationData { get; set; }
        public Guid? EmailTemplateId { get; set; }
    }
}
