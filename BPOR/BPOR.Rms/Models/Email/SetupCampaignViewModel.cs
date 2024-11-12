using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using Humanizer;
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
    public ContactMethods ContactMethod { get; set; }

    public int FilterCriteriaId { get; set; }

    public List<TemplateResponse> Templates { get; set; } = new List<TemplateResponse>();

    public string? SelectedTemplateId { get; set; }

    [Display(Name = "How many volunteers do you want to send it to?", Order = 3)]
    [Range(1, int.MaxValue, ErrorMessage = "Number of volunteers to be contacted must be 1 or more.")]
    public int? TotalVolunteers { get; set; }


    [Display(Name = "Preview email", Order = 2)]
    public string? PreviewEmails { get; set; }

    public IEnumerable<string> GetPreviewEmailAddresses() => PreviewEmails?.Split(_emailListDelimiters, StringSplitOptions.RemoveEmptyEntries)?.Select(x => x.Trim()) ?? Enumerable.Empty<string>();

    public string GetArticle(ContactMethods method)
    {
        return (method == ContactMethods.Email) ? "an" : "a";
    }
}
