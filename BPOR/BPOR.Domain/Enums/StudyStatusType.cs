using System.ComponentModel.DataAnnotations;

namespace BPOR.Domain.Enums;

public enum StudyStatusType
{
    [Display(Order = 0)]
    Draft = 0,
    
    [Display(Name = "New Application", Order = 1)]
    NewApplication = 1,
    
    [Display(Name = "In Progress", Order = 2)]
    InProgress = 2,
    
    [Display(Order = 3)]
    Active = 3,
    
    [Display(Name = "Concluded Successfully", Order = 4)]
    ConcludedSuccessfully = 4,
    
    [Display(Order = 5)]
    Rejected = 5,
    
    [Display(Order = 6)]
    Withdrawn = 6
}