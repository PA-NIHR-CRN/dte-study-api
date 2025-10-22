using System.ComponentModel.DataAnnotations;
using BPOR.Rms.Models.Filter;

namespace BPOR.Rms.Models.Volunteer;

public class UpdateAnonymousRecruitedViewModel
{
    public string? StudyName { get; set; }
    public int StudyId { get; set; }

    [Display(Name = "Latest recruitment total")]
    [Required(ErrorMessage = "Enter the total number of Be Part of Research volunteers recruited")]
    [IntegerOrDecimal(ErrorMessage = "Please enter a valid number")]
    [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
    public int? RecruitmentTotal { get; set; }
    public IEnumerable<EnrollmentDetails> EnrollmentDetails { get; set; } = new List<EnrollmentDetails>();
    public int? LatestRecruitmentTotal => EnrollmentDetails?.FirstOrDefault()?.RecruitmentTotal ?? 0;
    public bool HasCampaigns { get; set; }
}
