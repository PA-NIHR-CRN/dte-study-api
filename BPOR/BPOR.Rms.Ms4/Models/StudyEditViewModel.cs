using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Enums;
using NIHR.GovUk.AspNetCore.Mvc;

namespace BPOR.Rms.Ms4.Models;

public class StudyEditViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "Recruitment start date (UK)?",
        Description = "If you are unsure, provide an estimate.")]
    public GovUkDate RecruitmentStartDate { get; set; } = new();

    [Display(Name = "What is the UK recruitment target for the study?",
        Description = "If any participants have already been recruited, exclude these from the target.")]
    public string? RecruitmentTarget { get; set; }
    
    [Display(Name = "Is this study recruiting identifiable participants?")]
    public bool IsRecruitingIdentifiableParticipants { get; set; }

    [Display(Name = "Outcome of submission")]
    public int? SubmissionOutcome { get; set; }

    [Display(Name = "Website link?")]
    public string? InformationUrl { get; set; }

    [Display(Name = "NIHR funding stream or grant code")]
    public string? FundingCode { get; set; }

    [Display(Name = "Pre-screener link")]
    public string? PreScreenerUrl { get; set; }
    
    [Display(Name = "Update study status")]
    public StudyStatusType? Status { get; set; }
    
    public StudyStatusType? CurrentStatus { get; set; }
    
    [Display(Name = "Rejection reason")]
    public List<RejectedReasonType> RejectedReasons { get; set; } = [];
    public string? RejectedMiscReason { get; set; }

    [Display(Name = "Withdrawal reason")]
    public List<WithdrawnReasonType> WithdrawnReasons { get; set; } = [];
    public string? WithdrawnOtherReason { get; set; }
}