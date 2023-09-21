namespace StudyApi.Requests.Users;

public class SetUpMfaRequest
{
    public string MfaDetails { get; set; }
    public string PhoneNumber { get; set; }
    
}