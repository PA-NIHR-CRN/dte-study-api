using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Study;

public class StudyDetailsViewModel
{
    public StudyModel Study { get; set; }
    public NotificationBannerModel Notification { get; set; }
}

