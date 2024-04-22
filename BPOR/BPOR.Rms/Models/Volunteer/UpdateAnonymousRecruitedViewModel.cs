using System.ComponentModel.DataAnnotations;
using BPOR.Rms.Models.Filter;

namespace BPOR.Rms.Models.Volunteer;

public class UpdateAnonymousRecruitedViewModel
{
    public string StudyName { get; set; }
    public int StudyId { get; set; }

    [Display(Name = "Latest Recruitment Total")]
    [Required(ErrorMessage = "Please enter the total number of volunteers recruited")]
    [IntegerOrDecimal(ErrorMessage = "Please enter a valid number")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
    public int? RecruitmentTotal { get; set; }
    public IEnumerable<EnrollmentDetails> EnrollmentDetails { get; set; } = new List<EnrollmentDetails>();
    public NotificationBannerModel Notification { get; set; }
    public int? LatestRecruitmentTotal => EnrollmentDetails?.FirstOrDefault()?.RecruitmentTotal ?? 0;

    
}
