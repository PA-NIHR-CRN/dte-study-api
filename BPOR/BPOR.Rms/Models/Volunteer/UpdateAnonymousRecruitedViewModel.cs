using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Volunteer;

public class UpdateAnonymousRecruitedViewModel
{
    public string StudyName { get; set; }
    public int StudyId { get; set; }
    [Display(Name = "Total Volunteers")]
    public int LatestRecruitmentTotal { get; set; }
}
