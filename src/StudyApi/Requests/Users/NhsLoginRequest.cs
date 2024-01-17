namespace StudyApi.Requests.Users
{
    public class NhsLoginRequest
    {
        public string Code { get; set; }
        public string RedirectUrl { get; set; }
        public string SelectedLocale { get; set; }
    }
}