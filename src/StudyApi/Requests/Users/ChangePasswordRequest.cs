namespace StudyApi.Requests.Users
{
    public class ChangePasswordRequest
    {
        public string AccessToken { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}