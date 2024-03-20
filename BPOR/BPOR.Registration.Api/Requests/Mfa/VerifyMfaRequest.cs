namespace BPOR.Registration.Api.Requests.Mfa;

public class VerifyMfaRequest
{
    public string AuthenticatorAppCode { get; set; }
    public string MfaDetails { get; set; }
    public string SessionId { get; set; }
}
