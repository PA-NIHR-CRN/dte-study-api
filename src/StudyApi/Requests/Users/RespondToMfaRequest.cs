namespace StudyApi.Requests.Users;

public class RespondToMfaRequest
{
    public string MfaCode { get; set; }
    public string MfaDetails { get; set; }
}