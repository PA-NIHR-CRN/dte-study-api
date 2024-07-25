namespace BPOR.Rms
{
    public class RmsSettings
    {
        /// <summary>
        /// Email address to send the email campaign sent notification to.
        /// </summary>
        public string CampaignNotificationEmailAddress { get; set; } = "bepartofresearch-volunteerservice@nihr.ac.uk";
        public string StudyInformationFallbackUrl { get; set; } = "https://bepartofresearch.nihr.ac.uk/notfound";
    }
}
