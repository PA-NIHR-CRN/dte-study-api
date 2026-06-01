using System.ComponentModel.DataAnnotations;
using BPOR.Rms.Models.PreScreenerEligibility;

namespace BPOR.Rms.Models.ResearcherEmail;

public class ResearcherEmailViewModel
{
    public long StudyId { get; set; }
    [Required(ErrorMessage = "Please select an option.")]
    public string? SelectedEmail { get; set; }
    public bool IsEligibilityCriteriaComplete { get; set; }
    public bool IsEligibleForPrescreener { get; set; }
    
    public List<Dictionary<string, string>> ResearcherEmailOptions
    {
        get
        {
            return Constants.ResearcherEmails.getResearcherEmailOptions();
        }
    }
    
    public PreScreenerEligibilityViewModel PreScreenerViewModel
    {
        get
        {
            return new PreScreenerEligibilityViewModel
            {
                IsEligibilityCriteriaComplete = IsEligibilityCriteriaComplete,
                IsEligibleForPrescreener = IsEligibleForPrescreener
            };
        }
    }
}