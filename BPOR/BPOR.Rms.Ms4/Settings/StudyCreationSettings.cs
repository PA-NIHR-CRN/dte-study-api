namespace BPOR.Rms.Ms4.Settings;

public class StudyCreationSettings
{
    public string ResearcherCreationReturnToStepByStepGuideUrl { get; set; } = "https://bepartofresearch.nihr.ac.uk/StudyRegistration";

    public string ResearcherCreationFinishRedirectUrl { get; set; } = "https://bepartofresearch.nihr.ac.uk/";

    public string ResearcherCreationTermsUrl { get; set; } = "https://bepartofresearch.nihr.ac.uk/site-policies/terms-and-conditions/";
    
    public string StaleDraftRemovalSchedule { get; set; } = "0 0 2 * * ?"; // Every day at 02:00
    
    public TimeSpan StaleDraftAge { get; set; } =  TimeSpan.FromDays(5);
    
    public bool EnableMs4ResearcherJourney { get; set; } = true;
}