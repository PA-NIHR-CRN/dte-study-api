namespace StudyApi.Requests.Users
{
    public class ConfirmForgotPasswordRequest
    {
        public string Code { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}