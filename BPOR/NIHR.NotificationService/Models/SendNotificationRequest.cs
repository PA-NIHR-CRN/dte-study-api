namespace NIHR.NotificationService.Models;

public class SendNotificationRequest
{
    public string Reference { get; set; }
    public Dictionary<string, string> Personalisation { get; set; }
    public string TemplateId { get; set; }
    public string? EmailAddress { get; set; }

    public void Validate()
    {

        if (!Personalisation.TryGetValue("address_line_1", out var addressLine1))
        {
            throw new KeyNotFoundException("address_line_1 not found in personalisation data.");
        }

        if (!Personalisation.TryGetValue("campaignTypeId", out var campaignTypeIdStr))
        {
            throw new KeyNotFoundException("campaignTypeId not found in personalisation data.");
        }

        if (!int.TryParse(campaignTypeIdStr, out var campaignTypeId))
        {
            throw new ArgumentException($"campaignTypeId '{campaignTypeIdStr}' is not a valid integer.");
        }

        var contactMethod = (ContactMethod)campaignTypeId;


        switch (contactMethod)
        {
            case ContactMethod.Email:
                if (string.IsNullOrWhiteSpace(EmailAddress))
                    throw new ArgumentException("EmailAddress is required for email notifications.");
                if (string.IsNullOrWhiteSpace(TemplateId))
                    throw new ArgumentException("TemplateId is required for email notifications.");
                break;

            case ContactMethod.Letter:
                if (string.IsNullOrWhiteSpace(addressLine1))
                    throw new ArgumentException("Address is required for letter notifications.");
                if (string.IsNullOrWhiteSpace(TemplateId))
                    throw new ArgumentException("TemplateId is required for letter notifications.");
                break;

            default:
                throw new NotSupportedException($"Contact method {contactMethod} is not supported.");
        }
    }
}

public enum ContactMethod
{
    Email = 1,
    Letter = 2
}
