using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Study;

public class StudyFormViewModel
{
    public int Step { get; set; } = 1;

    [Required(ErrorMessage = "Please enter a name")]
    [Display(Name = "Main contact name for study")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Please enter an email address")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [Display(Name = "Main contact email address")]
    public string EmailAddress { get; set; }
    [Required(ErrorMessage = "Please enter a study name")]
    [Display(Name = "Study Name")]
    public string StudyName { get; set; }

    [Display(Name = "Anonymous enrolment")]
    public bool AnonymousEnrolment { get; set; }

    [Display(Name = "CPMS ID")] public long CpmsId { get; set; }
}
