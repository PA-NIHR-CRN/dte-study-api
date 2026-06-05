using System.ComponentModel.DataAnnotations;
using BPOR.Rms.Models.PreScreenerEligibility;

namespace BPOR.Rms.Models.ResearcherEmail;

public class ResearcherEmailViewModel
{
    public int StudyId { get; set; }
    public int SelectedEmailId { get; set; }
    public bool IsEligibilityCriteriaComplete { get; set; }
    public bool IsEligibleForPrescreener { get; set; }
    public int ResearcherUserId { get; set; }
    
    public List<Dictionary<string, string>> ResearcherEmailOptions
    {
        get
        {
            return Constants.ResearcherEmails.GetResearcherEmailOptions();
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