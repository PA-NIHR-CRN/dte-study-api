namespace NIHR.NotificationService.Models;

public class SendNotificationRequest
{
    public string Reference { get; set; }
    public Dictionary<string, string> Personalisation { get; set; }
    public ContactMethod ContactMethod { get; set; }

    // TODO: split into Email and Letter TemplateId?
    public string TemplateId { get; set; }

    public string? EmailAddress { get; set; }
    public string? Address { get; set; }

    public void Validate()
    {
        switch (ContactMethod)
        {
            case ContactMethod.Email:
                if (string.IsNullOrWhiteSpace(EmailAddress))
                    throw new ArgumentException("EmailAddress is required for email notifications.");
                if (string.IsNullOrWhiteSpace(TemplateId))
                    throw new ArgumentException("TemplateId is required for email notifications.");
                break;

            case ContactMethod.Letter:
                if (string.IsNullOrWhiteSpace(Address))
                    throw new ArgumentException("Address is required for letter notifications.");
                if (string.IsNullOrWhiteSpace(TemplateId))
                    throw new ArgumentException("TemplateId is required for letter notifications.");
                break;

            default:
                throw new NotSupportedException($"Contact method {ContactMethod} is not supported.");
        }
    }
}

public enum ContactMethod
{
    Email,
    Letter
}
