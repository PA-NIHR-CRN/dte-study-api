using System.ComponentModel;
using BPOR.Rms.Models.Researcher;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Study;

public class StudyFormEditModel : FormWithSteps
{
    public override int TotalSteps => 2;

    [Display(Name = "Name of main contact for the study", Order = 1)]
    public string FullName { get; set; }

    [Display(Name = "Email address of main contact for the study", Order = 2)]
    public string EmailAddress { get; set; }
    
    [Display(Name = "Study name", Order = 3)]
    public string StudyName { get; set; }

    [Display(Name = "CPMS ID", Order = 5)]
    public long? CpmsId { get; set; }

    [Display(Name = "Is this study recruiting identifiable participants?", Order = 4)]
    public bool? IsRecruitingIdentifiableParticipants { get; set; }

    [Display(Name = "Information URL", Order = 6, Description = "Provide the information URL for the study. A link to this URL will be included in campaign emails.")]
    public string? InformationUrl { get; set; }

    public override String StepName
    {
        get
        {
            return "Add a study";
        }
    }
    
    public bool AllowEditIsRecruitingIdentifiableParticipants { get; set; }
}
