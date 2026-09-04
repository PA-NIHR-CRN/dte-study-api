using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Enums;
using BPOR.Rms.Utilities;
using NIHR.Infrastructure.AspNetCore;

namespace BPOR.Rms.Models.Study;

public class StudyModel
{
    [Display(Name = "Study ID")] public int Id { get; set; }

    [Required]
    [Display(Name = "Primary contact")]
    [StudyEdit(1)]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Primary contact email address")]
    [StudyEdit(1)]
    public string EmailAddress { get; set; }

    [Required]
    [Display(Name = "Study name")]
    [StudyEdit(2)]
    [ResearcherEdit(1)]
    public string StudyName { get; set; }

    [Display(Name = "CPMS ID")]
    [StudyEdit(2)]
    [ResearcherEdit(3)] 
    public long? CpmsId { get; set; }

    [Display(Name = "Is this study recruiting identifiable participants?")]
    [StudyEdit(2)]
    [ResearcherEdit(3)]
    public bool IsRecruitingIdentifiableParticipants { get; set; }
    public int? LatestRecruitmentTotal { get; set; }
    public int? TotalRecruited { get; set; }

    [Display(Name = "Who is the Chief Investigator for the study?")]
    [ResearcherEdit(1)]
    public string? ChiefInvestigator { get; set; }

    [Display(Name = "Sponsor organisation")]
    [ResearcherEdit(1)]
    public string? StudySponsors { get; set; }

    [Display(Name = "Have you applied for inclusion in the RDN portfolio?")]
    [ResearcherEdit(2)]
    public string? PortfolioSubmissionStatus { get; set; }

    [Display(Name = "Outcome of submission")]
    [ResearcherEdit(3)]
    public string? OutcomeOfSubmission { get; set; }

    public NihrFundingStatusType? HasFunding { get; set; }

    [Display(Name = "NIHR funding stream or grant code")]
    [ResearcherEdit(5)]
    public string? FundingCode { get; set; }

    [Display(Name = "What is the UK recruitment target for the study?")]
    [ResearcherEdit(6)]
    public string? UKRecruitmentTarget { get; set; }

    [Display(Name = "What is the target population for the study?")]
    [ResearcherEdit(6)]
    public string? TargetPopulation { get; set; }

    [Display(Name = "Recruitment start date")]
    [ResearcherEdit(7)]
    public string? RecruitmentStartDate { get; set; }

    [Display(Name = "Recruitment end date")]
    [ResearcherEdit(7)]
    public string? RecruitmentEndDate { get; set; }

    [Display(Name = "Does the study have NIHR funding?")]
    [ResearcherEdit(4)]
    public string? HasFundingDisplay => HasFunding == null ? null : (HasFunding == NihrFundingStatusType.Yes ? "Yes" : "No");

    [Display(Name = "Website link")]
    [StudyEdit(3)]
    public string? InformationUrl { get; set; }
    
    [ValueDisplayFormatter(typeof(YesNoFormatter))]
    [Display(Name = "Will this study have more than one research location in the UK?")]
    [StudyEdit(4)]
    public bool? HasMultipleResearchLocations { get; set; }
    

    [ValueDisplayFormatter(typeof(YesNoFormatter))]
    [Display(Name = "Will one person be responsible for recruiting or screening for this study using Be Part of Research?")]
    [StudyEdit(5)]
    public bool? SinglePersonResponsibleForRecruiting { get; set; }
    
    [Display(Name = "Pre-screener link")]
    [StudyEdit(6)]
    public string? PreScreenerUrl { get; set; }
    

    public bool IsEligibilityCriteriaComplete =>
        HasMultipleResearchLocations.HasValue && SinglePersonResponsibleForRecruiting.HasValue;

    public bool IsEligibleForPrescreener =>
        IsEligibilityCriteriaComplete && !(HasMultipleResearchLocations!.Value && SinglePersonResponsibleForRecruiting!.Value);

    [Display(Name = "Volunteer study information page")]
    public string? VolunteerInformationUrl { get; set; }
}

