using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Study;

public class StudyModel
{
    [Display(Name = "Study ID")] public int Id { get; set; }

    [Required]
    [Display(Name = "Main contact")]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email address")]
    public string EmailAddress { get; set; }

    [Required]
    [Display(Name = "Study name")]
    public string StudyName { get; set; }

    [Display(Name = "CPMS ID")] public long? CpmsId { get; set; }

    [Display(Name = "Is this study recruiting identifiable participants?")]
    public bool IsRecruitingIdentifiableParticipants { get; set; }
    public int? LatestRecruitmentTotal { get; set; }
    public int? TotalRecruited { get; set; }
    
    [Display(Name = "Who is the Chief Investigator for the study?")]
    public string? ChiefInvestigator { get; set; }

    [Display(Name = "Provide the name(s) of the study sponsor(s), funder(s) and CRO (if applicable)")]
    public string? StudySponsors { get; set; }

    [Display(Name = "Has the study been submitted for inclusion on the NIHR CRN portfolio?")]
    public string? PortfolioSubmissionStatus { get; set; }
    [Display(Name = "Outcome of submission")]
    public string? OutcomeOfSubmission { get; set; }

    [Display(Name = "CPMS ID")]
    public long? CPMSId { get; set; }

    [Display(Name = "Does the study have NIHR funding?")]
    public bool? HasFunding { get; set; }

    [Display(Name = "NIHR funding stream or grant code")]
    public string? FundingCode { get; set; }

    [Display(Name = "What is the UK recruitment target for the study?")]
    public string? UKRecruitmentTarget { get; set; }

    [Display(Name = "What is the target population for the study?")]
    public string? TargetPopulation { get; set; }
    // TODO 2 GovUkDates, need to amalgamate
    [Display(Name = "Recruitment start date")]
    public DateOnly? RecruitmentStartDate { get; set; } = new();
    [Display(Name = "Recruitment end date")]
    public DateOnly? RecruitmentEndDate { get; set; } = new();

}
