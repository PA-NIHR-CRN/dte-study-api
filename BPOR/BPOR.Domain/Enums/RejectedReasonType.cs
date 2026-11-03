using System.ComponentModel.DataAnnotations;

namespace BPOR.Domain.Enums;

public enum RejectedReasonType
{
    [Display(Name = "Not NIHR-affiliated")]
    NotNihrAffiliated = 1,
    [Display(Name = "PPIE opportunity")]
    PpieOpportunity = 2,
    [Display(Name = "Study already listed here")]
    StudyAlreadyListedHere = 3,
    [Display(Name = "Not possible to recruit target population")]
    NotPossibleToRecruitTargetPopulation = 4,
    [Display(Name = "Recruitment window too short")]
    RecruitmentWindowTooShort = 5,
    Misc = 6
}