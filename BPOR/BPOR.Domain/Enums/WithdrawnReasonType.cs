using System.ComponentModel.DataAnnotations;

namespace BPOR.Domain.Enums;

public enum WithdrawnReasonType
{
    [Display(Name = "No response from study team")]
    NoResponseFromStudyTeam = 1,
    [Display(Name = "Study does not need additional support")]
    StudyDoesNotNeedAdditionalSupport = 2,
    [Display(Name = "Problems with Study")]
    ProblemsWithStudy = 3,
    [Display(Name = "Study team has limited capacity")]
    StudyTeamHasLimitedCapacity = 4,
    [Display(Name = "Contact dropped by BPoR team")]
    ContactDroppedByBPorTeam = 5,
    Other = 6
}