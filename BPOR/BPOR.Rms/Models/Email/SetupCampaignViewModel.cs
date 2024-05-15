using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Email;

public class SetupCampaignViewModel
{
    public int? StudyId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a number greater than zero for Max Numbers.")]
    public int MaxNumbers { get; set; }

    public string? StudyName { get; set; }

    public int FilterCriteriaId { get; set; }


    [DisplayName("Select email template")]
    public string? SelectedTemplateId { get; set; }
    public IEnumerable<SelectListItem> EmailTemplates { get; set; } = new List<SelectListItem>();


    [DisplayName("How many volunteers do you want to send it to?")]
    [Range(1, int.MaxValue, ErrorMessage = "Total Volunteers must be at least 1.")]
    public int? TotalVolunteers { get; set; }


    [DisplayName("Preview email")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string? PreviewEmails { get; set; }
}
