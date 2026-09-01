using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Enums;

namespace BPOR.Rms.Ms4.Models;

public class StudyRequestViewModel
{
    public int? StudyId { get; set; }

    [Display(Name = "Do you have ethics approval to use Be Part of Research?")]
    public bool? HasEthicsApproval { get; set; }
    
    public SubmittedType? InclusionInRdnPortfolioStatus { get; set; }
    [Display(Name = "Have you applied for inclusion in the RDN portfolio")]
    public string? InclusionInRdnPortfolioStatusDisplay { get; set; }
    
    public NihrFundingStatusType? NihrFundingStatus { get; set; }
    [Display(Name = "Does this study have NIHR funding?")]
    public string? NihrFundingStatusDisplay { get; set; }
    
    [Display(Name = "What is your CPMS ID?")]
    public long? CpmsId { get; set; }
    
    [Display(Name = "When will you finish recruiting to this study?")]
    public DateTime? RecruitmentEndDate { get; set; }
    public int? FinishRecruitingDay { get; set; }
    public int? FinishRecruitingMonth { get; set; }
    public int? FinishRecruitingYear { get; set; }
    
    [Display(Name = "What is the title of your study?")]
    public string? StudyTitle { get; set; }
    
    [Display(Name = "Provide a one-line description of your study")]
    public string? StudyDescription { get; set; }
    
    [Display(Name = "Will this study have more than one research location?")]
    public bool? HasMultipleResearchLocations { get; set; }
    
    [Display(Name = "Will this study be managed by more than one person?")]
    public bool? SinglePersonResponsibleForRecruiting { get; set; }
    
    [Display(Name = "Who is the chief investigator for your study?")]
    public string? ChiefInvestigatorName { get; set; }
    public string? ChiefInvestigatorEmail { get; set; }
    public bool? IsChiefInvestigatorMainContact { get; set; }
    
    [Display(Name = "Who is the main point of contact for the study")]
    public string? MainContactName { get; set; }
    public string? MainContactEmail { get; set; }
    public string? MainContactRole { get; set; }
    
    [Display(Name = "Select the Sponsor Organisation")]
    public string? SponsorName { get; set; }

    [Display(Name = "Who will be included in this study?")]
    public string? InclusionCriteria { get; set; }
}