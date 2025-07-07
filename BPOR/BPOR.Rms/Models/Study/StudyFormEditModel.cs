using BPOR.Rms.Models.Researcher;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Study;

public class StudyFormEditModel : FormWithSteps
{
    public override int TotalSteps => 2;

    [Required(ErrorMessage = "Enter the name of the main contact for the study")]
    [Display(Name = "Name of main contact for the study", Order = 1)]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Enter the email address of the main contact for the study")]
    [EmailAddress(ErrorMessage = "Enter an email address in the correct format, like name@example.com")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Enter an email address in the correct format, like name@example.com")]
    [Display(Name = "Email address of main contact for the study", Order = 2)]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "Enter the study name")]
    [Display(Name = "Study name", Order = 3)]
    public string StudyName { get; set; }

    [Display(Name = "CPMS ID", Order = 5)]
    [RegularExpression(@"^\d+$", ErrorMessage = "Enter a CPMS ID in the correct format, like 12345")]
    public long? CpmsId { get; set; }

    [Display(Name = "Is this study recruiting identifiable participants?", Order = 4)]
    [Required(ErrorMessage = "Select whether the study is recruiting identifiable participants")]
    public bool? IsRecruitingIdentifiableParticipants { get; set; }

    [Display(Name = "Information URL", Order = 6)]
    public string? InformationUrl { get; set; }

    public override String StepName
    {
        get
        {
            return "Add a study";
        }
    }
}
