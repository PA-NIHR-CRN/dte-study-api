using System.ComponentModel.DataAnnotations;

namespace BPOR.Domain.Enums;

public enum StudyStatusType
{
    Draft = 0,
    [Display(Name = "New Application")]
    NewApplication = 1,
    [Display(Name = "In Progress")]
    InProgress = 2,
    Active = 3,
    [Display(Name = "Concluded Successfully")]
    ConcludedSuccessfully = 4,
    Rejected = 5,
    Withdrawn = 6
}