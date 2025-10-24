using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Volunteer;

public class UpdateRecruitedViewModel : UpdateRecruitedBase
{
    [Display(Name = "Enter Be Part of Research volunteer reference numbers")]
    [Required(ErrorMessage = "Enter a Be Part of Research volunteer reference number")]
    public string? VolunteerReferenceNumbers { get; set; }
}
