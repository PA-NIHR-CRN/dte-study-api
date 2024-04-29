using System;
using System.Collections.Generic;

namespace NIHR.Infrastructure.Models
{
    public class SendBatchEmailRequest
    {
        public ICollection<string> EmailAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Guid? EmailTemplateId { get; set; }
        
    }
}
