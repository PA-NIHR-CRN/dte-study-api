namespace NIHR.Infrastructure.Models
{
    public class SendEmailRequest
    {
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        
    }
}
