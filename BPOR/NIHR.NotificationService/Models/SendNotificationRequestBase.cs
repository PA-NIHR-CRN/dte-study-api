using NIHR.NotificationService.Enums;

namespace NIHR.NotificationService.Models;

public class SendNotificationRequestBase
{
    public Dictionary<string, string> Personalisation { get; set; } = new();
    public string TemplateId { get; set; } = string.Empty;
    public NotificationContactMethod ContactMethod { get; set; }
    
    private bool HasPersonalisation(string key) => 
        Personalisation.TryGetValue(key, out var value) || !string.IsNullOrWhiteSpace(value);

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(TemplateId))
        {
            throw new ArgumentException("TemplateId is required for all notifications.");
        }

        string[] requiredPersonalisations = ContactMethod switch
        {
            NotificationContactMethod.Email => [PersonalisationKeys.Email],
            NotificationContactMethod.Letter =>
                [PersonalisationKeys.AddressLine1, PersonalisationKeys.AddressLine5, PersonalisationKeys.Postcode],
            _ => throw new ArgumentOutOfRangeException()
        };
            
        var missingPersonalsations = requiredPersonalisations.Where(i => !HasPersonalisation(i)).ToArray();
            
        if (missingPersonalsations.Any())
        {
            throw new ArgumentException($"{string.Join(", ", missingPersonalsations)} is required for {ContactMethod} notifications.");
        }
    }
}