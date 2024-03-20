namespace BPOR.Registration.Api.Requests.Password;

public class ConfirmForgotPasswordRequest
{
    public string Code { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }
}
