namespace NIHR.NotificationService.Models;

public class SendNotificationRequest
{
    // Common fields
    public string Reference { get; set; }
    public Dictionary<string, string> Personalisation { get; set; }
    public ContactMethod ContactMethod { get; set; }

    // Email fields
    public string? EmailAddress { get; set; }
    public string? EmailTemplateId { get; set; }

    // Letter fields
    public string? Address { get; set; }
    public string? LetterTemplateId { get; set; }

    public void Validate()
    {
        switch (ContactMethod)
        {
            case ContactMethod.Email:
                if (string.IsNullOrWhiteSpace(EmailAddress))
                    throw new ArgumentException("EmailAddress is required for email notifications.");
                if (string.IsNullOrWhiteSpace(EmailTemplateId))
                    throw new ArgumentException("EmailTemplateId is required for email notifications.");
                break;

            case ContactMethod.Letter:
                if (string.IsNullOrWhiteSpace(Address))
                    throw new ArgumentException("Address is required for letter notifications.");
                if (string.IsNullOrWhiteSpace(LetterTemplateId))
                    throw new ArgumentException("LetterTemplateId is required for letter notifications.");
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
