namespace BPOR.Rms
{
    public class RmsSettings
    {
        /// <summary>
        /// Email address to send the email campaign sent notification to.
        /// </summary>
        public string CampaignNotificationEmailAddress { get; set; } = "bepartofresearch-volunteerservice@nihr.ac.uk";
        public string StudyInformationFallbackUrl { get; set; } = "https://bepartofresearch.nihr.ac.uk/notfound";
        
        public string ReasearchIntroductoryTemplateId { get; set; }
        public string ReasearchNextStepsWithPrescreenerTemplateId { get; set; }
        public string ReasearchNextStepsWithoutPrescreenerTemplateId { get; set; }
    }
}
