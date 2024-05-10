using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Volunteer;

public class UpdateRecruitedViewModel
{
    [Display(Name = "Enter Be Part of Research volunteer reference numbers")]
    [Required(ErrorMessage = "Volunteer reference numbers must be provided")]
    public string? VolunteerReferenceNumbers { get; set; }
    public string StudyName { get; set; }
    public int StudyId { get; set; }
    public NotificationBannerModel Notification { get; set; }
}
