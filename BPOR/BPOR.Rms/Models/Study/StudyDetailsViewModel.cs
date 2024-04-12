using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Study;

public class StudyDetailsViewModel
{
    public StudyModel Study { get; set; }
    public string AnonymousEnrolmentDisplay => Study.AnonymousEnrolment ? "Yes" : "No";
    public NotificationBannerModel Notification { get; set; }
}

