namespace NIHR.Infrastructure.Settings
{
    public class ContentfulSettings
    {
        public static readonly string SectionName = nameof (ContentfulSettings);
        public string ContentType { get; set; }

        public string DeliveryApiKey { get; set; }

        public string PreviewApiKey { get; set; }

        public string SpaceId { get; set; }

        public bool UsePreviewApi { get; set; }

        public string BaseUrl { get; set; }

    }
}
