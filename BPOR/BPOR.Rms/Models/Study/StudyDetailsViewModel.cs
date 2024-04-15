using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Study;

public class StudyDetailsViewModel
{
    public StudyModel Study { get; set; }
    public string IsRecruitingIdentifiableParticipantsDisplay => Study.IsRecruitingIdentifiableParticipants ? "Yes" : "No";
    public NotificationBannerModel Notification { get; set; }
}

