using BPOR.Domain.Enums;
using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.VolunteerInformation.Models;

public class CreateCriterionModel : CreateCriterionPostbackModel
{
    public string GroupName { get; set; }
    public VsiGroupCriteronType Type { get; set; }
}