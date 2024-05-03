using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Entities;

namespace BPOR.Rms.Models.Email;

public class SetupCampaignViewModel
{
    public int? StudyId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a number greater than zero for Max Numbers.")]
    public int MaxNumbers { get; set; }

    [Required(ErrorMessage = "Please enter the total number of volunteers you want to send the email to.")]
    [DisplayName("How many volunteers do you want to send it to?")]
    [Range(1, int.MaxValue, ErrorMessage = "Total Volunteers must be at least 1.")]
    public int? TotalVolunteers { get; set; }

    public string? StudyName { get; set; }

    [DisplayName("Select email template")]
    [Required(ErrorMessage = "Please select a email template.")]
    public string? SelectedTemplateId { get; set; }
    public string? SelectedTemplateName { get; set; }

    [DisplayName("Preview email")]
    [Required(ErrorMessage = "Enter at least one address")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string? PreviewEmails { get; set; }

    public NotificationBannerModel? Notification { get; set; }
    public int FilterCriteriaId { get; set; }
    public IEnumerable<EmailTemplate> EmailTemplates { get; set; } = new List<EmailTemplate>();
}

public class EmailTemplate
{
    public string Id { get; set; }
    public string Name { get; set; }
}
