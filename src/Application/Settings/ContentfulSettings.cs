namespace Application.Settings
{
    public class ContentfulSettings
    {
        public const string SectionName = "ContentfulSettings";
        public string DeliveryApiKey { get; set; }
        public string PreviewApiKey { get; set; }
        public string SpaceId { get; set; }
        public bool UsePreviewApi { get; set; }
        public string BaseUrl { get; set; }
        public EmailTemplates EmailTemplates { get; set; }
    }
    public class EmailTemplates
    {
        public string EmailAccountExists { get; set; }
        public string NhsAccountExists { get; set; }
        public string NhsSignUp { get; set; }
        public string NhsPasswordReset { get; set; }
    }
}