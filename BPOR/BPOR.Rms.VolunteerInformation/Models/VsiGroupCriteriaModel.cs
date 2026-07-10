using BPOR.Domain.Enums;
using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.VolunteerInformation.Models;

public class VsiGroupCriteriaModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public VsiGroupCriteronType Type { get; set; }
}