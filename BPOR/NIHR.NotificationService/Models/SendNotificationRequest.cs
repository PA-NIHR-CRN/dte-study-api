using BPOR.Domain.Enums;

namespace NIHR.NotificationService.Models;

public class SendNotificationRequest
{
    public string Reference { get; set; }
    public Dictionary<string, string> Personalisation { get; set; }
    public string TemplateId { get; set; }
    public string? EmailAddress { get; set; }

    public void Validate()
    {

        if (!Personalisation.TryGetValue("campaignTypeId", out var campaignTypeIdStr))
        {
            throw new KeyNotFoundException("campaignTypeId not found in personalisation data.");
        }

        if (!int.TryParse(campaignTypeIdStr, out var campaignTypeId))
        {
            throw new ArgumentException($"campaignTypeId '{campaignTypeIdStr}' is not a valid integer.");
        }

        var contactMethod = (ContactMethods)campaignTypeId;


        switch (contactMethod)
        {
            case ContactMethods.Email:
                if (string.IsNullOrWhiteSpace(EmailAddress))
                    throw new ArgumentException("EmailAddress is required for email notifications.");
                if (string.IsNullOrWhiteSpace(TemplateId))
                    throw new ArgumentException("TemplateId is required for email notifications.");
                break;

            case ContactMethods.Letter:
                if (!Personalisation.TryGetValue("address_line_1", out var addressLine1) || string.IsNullOrWhiteSpace(addressLine1))
                {
                    throw new ArgumentException("Address line 1 is required for letter notifications.");
                }
                if (string.IsNullOrWhiteSpace(TemplateId))
                    throw new ArgumentException("TemplateId is required for letter notifications.");
                break;

            default:
                throw new NotSupportedException($"Contact method {contactMethod} is not supported.");
        }
    }
}