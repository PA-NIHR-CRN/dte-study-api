using System.ComponentModel.DataAnnotations;

namespace BPOR.Domain.Enums;

public enum EthicsApproval
{
    [Display (Name = "Not yet, I am awaiting an approval")]
    NoButApplied,
    [Display (Name = "Yes")]
    Yes,
}