namespace StudyApi.Requests.Users
{
    public class ConfirmForgotPasswordRequest
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}