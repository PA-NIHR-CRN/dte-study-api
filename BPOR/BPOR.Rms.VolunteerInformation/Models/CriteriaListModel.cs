using BPOR.Domain.Enums;

namespace BPOR.Rms.VolunteerInformation.Models;

public class CriteriaListModel
{
    public VsiGroupModel VsiGroup { get; set; }
    
    public VolunteerStudyInformationGroupCriteriaTypeId Type { get; set; }
}