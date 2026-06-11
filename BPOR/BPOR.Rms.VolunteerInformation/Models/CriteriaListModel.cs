using BPOR.Domain.Enums;
using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.VolunteerInformation.Models;

public class CriteriaListModel
{
    public VsiGroupModel VsiGroup { get; set; }
    
    public VsiGroupCriteronType Type { get; set; }
}