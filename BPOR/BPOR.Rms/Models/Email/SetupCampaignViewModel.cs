using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Notify.Models.Responses;

namespace BPOR.Rms.Models.Email;

public class SetupCampaignViewModel
{
    private static readonly char[] _emailListDelimiters = [',', ';'];

    public int? StudyId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a number greater than zero for Max Numbers.")]
    public int MaxNumbers { get; set; }

    public string? StudyName { get; set; }
    public string? EmailAddress { get; set; }

    public string? ContactPreference { get; set; }

    public int FilterCriteriaId { get; set; }


    [Display(Name = "Select email template", Order = 1)]
    public string? SelectedTemplateId { get; set; }
    public TemplateList EmailTemplates { get; set; } = new ();


    [Display(Name = "How many volunteers do you want to send it to?", Order = 3)]
    [Range(1, int.MaxValue, ErrorMessage = "Total Volunteers must be at least 1.")]
    public int? TotalVolunteers { get; set; }


    [Display(Name ="Preview email", Order = 2)]
    public string? PreviewEmails { get; set; }
    
    public IEnumerable<string> GetPreviewEmailAddresses() => PreviewEmails?.Split(_emailListDelimiters, StringSplitOptions.RemoveEmptyEntries)?.Select(x=>x.Trim()) ?? Enumerable.Empty<string>();
}
