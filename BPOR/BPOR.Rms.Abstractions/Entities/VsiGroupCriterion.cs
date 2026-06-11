using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.Abstractions.Entities;

public class VsiGroupCriterion
{
    public int Id { get; set; }
    public string Description { get; set; }
    public VsiGroupCriteronType Type { get; set; }
}