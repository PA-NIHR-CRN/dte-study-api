namespace BPOR.Infrastructure.Models.Mfa;

public class TotpTokenResult
{
    public string SecretCode { get; set; }
    public string SessionId { get; set; }
    public string Username { get; set; }
}
