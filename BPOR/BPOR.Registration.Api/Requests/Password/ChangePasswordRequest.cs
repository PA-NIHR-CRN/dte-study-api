namespace BPOR.Registration.Api.Requests.Password;

public class ChangePasswordRequest
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
