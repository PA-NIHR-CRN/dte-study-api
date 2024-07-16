namespace BPOR.Registration.Api.Requests.Mfa;

public class RespondToMfaRequest
{
    public string MfaCode { get; set; }
    public string MfaDetails { get; set; }
}
