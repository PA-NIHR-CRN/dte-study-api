namespace NIHR.NotificationService;

// TODO: Figure out how to decouple this from GovUkNotify - so have standard keys for email, address, phone etc.;
// TODO: handle templating/routing in a provider-agnostic fashion;
public static class PersonalisationKeys
{
    public const string ContactMethod = "__contact_method";
    public const string TemplateId = "__template_id";
    public const string NotificationReference = "__notification_reference";
    
    public const string Email = "email";
    
    public const string AddressLine1 = "address_line_1";
    public const string AddressLine2 = "address_line_2";
    public const string AddressLine3 = "address_line_3";
    public const string AddressLine4 = "address_line_4";
    public const string AddressLine5 = "address_line_5";
    public const string AddressLine6 = "address_line_6";
    public const string Postcode = "address_postcode";

}