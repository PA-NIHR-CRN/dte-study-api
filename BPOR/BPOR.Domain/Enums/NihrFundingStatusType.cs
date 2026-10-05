using System.ComponentModel.DataAnnotations;

namespace BPOR.Domain.Enums;

public enum NihrFundingStatusType
{
    [Display(Name = "Yes", Order = 0)]
    Yes = 1,
    [Display(Name = "No", Order = 1)]
    No = 2,
    [Display(Name = "No, but I have applied for NIHR funding", Order = 2)]
    NoButApplied = 3
}