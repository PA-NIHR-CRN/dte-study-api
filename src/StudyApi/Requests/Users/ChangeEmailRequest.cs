namespace StudyApi.Requests.Users
{
    public class ChangeEmailRequest
    {
        public string AccessToken { get; set; }
        public string NewEmail { get; set; }
    }
}