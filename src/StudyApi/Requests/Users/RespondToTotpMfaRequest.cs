namespace StudyApi.Requests.Users;

public class RespondToTotpMfaRequest
{
    public string AuthenticatorAppCode { get; set; }
    public string MfaDetails { get; set; }
}