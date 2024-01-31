namespace Application.Models.MFA;

public class MfaLoginDetails
{
    public string Username { get; set; }
    public string SessionId { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}
