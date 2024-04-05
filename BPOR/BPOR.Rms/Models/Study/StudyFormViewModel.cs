using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Study;

public class StudyFormViewModel
{
    // TODO check if a neew model is needed for id vs no id
    public int Id { get; set; }
    public int Step { get; set; } = 1;

    [Required(ErrorMessage = "Enter the name of the main contact for the study")]
    [Display(Name = "Name of main contact for the study")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Enter the email address of the main contact for the study")]
    [EmailAddress(ErrorMessage = "Enter an email address in the correct format, like name@example.com")]
    [Display(Name = "Email address of main contact for the study")]
    public string EmailAddress { get; set; }
    [Required(ErrorMessage = "Enter the study name")]
    [Display(Name = "Study Name")]
    public string StudyName { get; set; }

    [Display(Name = "Is study enrolment anonymous?")]
    [Required(ErrorMessage = "Select whether study enrolment is anonymous")]
    public bool? AnonymousEnrolment { get; set; }

    [Display(Name = "CPMS ID")] 
    [RegularExpression(@"^\d+$", ErrorMessage = "Enter a CPMS ID in the correct format, like 12345")]
    public long? CpmsId { get; set; }
}
