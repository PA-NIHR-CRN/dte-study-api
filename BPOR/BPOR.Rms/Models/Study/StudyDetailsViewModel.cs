using System.ComponentModel.DataAnnotations;
using BPOR.Rms.Models.Volunteer;

namespace BPOR.Rms.Models.Study;

public class StudyDetailsViewModel
{
    public StudyModel Study { get; set; }
    public string IsRecruitingIdentifiableParticipantsDisplay => Study.IsRecruitingIdentifiableParticipants ? "Yes" : "No";
    public NotificationBannerModel Notification { get; set; }
    public IEnumerable<EnrollmentDetails> EnrollmentDetails { get; set; }
    public int TotalRecruited => EnrollmentDetails.Sum(e => e.RecruitmentTotal);
    public int LatestRecruitmentTotal => EnrollmentDetails.FirstOrDefault()?.RecruitmentTotal ?? 0;
}

