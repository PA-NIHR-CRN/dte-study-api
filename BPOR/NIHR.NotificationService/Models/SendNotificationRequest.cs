using NIHR.NotificationService.Enums;

namespace NIHR.NotificationService.Models;

public class SendNotificationRequest
{
    public NotificationReference Reference { get; set; }
    public Dictionary<string, string> Personalisation { get; set; } = new Dictionary<string, string>();
    public string TemplateId { get; set; } = string.Empty;
    public string? EmailAddress { get; set; }
    public GovUkNotifyContactMethod ContactMethod { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(TemplateId))
        {
            throw new ArgumentException("TemplateId is required for all notifications.");
        }

        switch (ContactMethod)
        {
            case GovUkNotifyContactMethod.Email:
                if (string.IsNullOrWhiteSpace(EmailAddress))
                {
                    throw new ArgumentException("EmailAddress is required for email notifications.");
                }

                if (string.IsNullOrWhiteSpace(TemplateId))
                {
                    throw new ArgumentException("TemplateId is required for email notifications.");
                }

                break;

            case GovUkNotifyContactMethod.Letter:
                if (!Personalisation.TryGetValue("address_line_1", out var addressLine1) || string.IsNullOrWhiteSpace(addressLine1))
                {
                    throw new ArgumentException("Address line 1 is required for letter notifications.");
                }
                if (string.IsNullOrWhiteSpace(TemplateId))
                {
                    throw new ArgumentException("TemplateId is required for letter notifications.");
                }

                break;

            default:
                throw new NotSupportedException($"Contact method {ContactMethod} is not supported.");
        }
    }
}