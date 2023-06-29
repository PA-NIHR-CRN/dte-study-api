namespace StudyApi.Requests.Users;

public class RespondToTotpMfaRequest
{
    public string AuthenticatorAppCode { get; set; }
    public string MfaDetails { get; set; }

    //public string SessionId { get; set; }
    //public string Username { get; set; }
}