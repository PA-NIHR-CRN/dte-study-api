using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Email;

public class SetupCampaignViewModel
{
    public int? StudyId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a number greater than zero for Max Numbers.")]
    public int MaxNumbers { get; set; }

    [DisplayName("How many volunteers do you want to send it to?")]
    [Range(1, int.MaxValue, ErrorMessage = "Total Volunteers must be at least 1.")]
    public int TotalVolunteers { get; set; }

    public string? StudyName { get; set; }

    public string SelectedTemplate { get; set; }

    [DisplayName("Preview email")]
    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string PreviewEmails { get; set; }
    public NotificationBannerModel? Notification { get; set; }
}
