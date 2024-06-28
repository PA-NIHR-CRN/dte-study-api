namespace NIHR.Infrastructure.Settings
{
    public class ContentfulSettings
    {
        public static readonly string SectionName = nameof(ContentfulSettings);
        public string DeliveryApiKey { get; set; }
        public string PreviewApiKey { get; set; }
        public string SpaceId { get; set; }
        public bool UsePreviewApi { get; set; }
        public string BaseUrl { get; set; }
        public string ContentType { get; set; }
        public EmailTemplates EmailTemplates { get; set; }
    }
    public class EmailTemplates
    {
        public string ResearcherOnboarding { get; set; }
    }
}
