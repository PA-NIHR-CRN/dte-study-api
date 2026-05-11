using BPOR.Domain.Enums;

namespace BPOR.Domain.Entities;

public class VolunteerStudyInformationGroupCriteria
{
    public int Id { get; set; }
    public long VolunteerStudyInformationGroupId { get; set; }
    public string Criteria { get; set; }
    
    public VolunteerStudyInformationGroup Group { get; set; }
    
    public VolunteerStudyInformationGroupCriteriaTypeId TypeId { get; set; }
}