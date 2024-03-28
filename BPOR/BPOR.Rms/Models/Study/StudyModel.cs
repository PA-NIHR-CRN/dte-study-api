using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Study;

public class StudyModel
{
    [Display(Name = "Study ID")]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Main contact")]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email address")]
    public string EmailAddress { get; set; }

    [Required]
    [Display(Name = "Study name")]
    public string StudyName { get; set; }

    [Display(Name = "CPMS ID")]
    public long CpmsId { get; set; }
}
