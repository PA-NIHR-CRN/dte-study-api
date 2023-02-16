namespace StudyApi.Requests.Users
{
    public class ConfirmSignUpRequest
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }
}