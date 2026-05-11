namespace BPOR.Domain.Entities;

public class VolunteerStudyInformationGroup
{
    public long Id { get; set; }
    public long VolunteerStudyInformationId { get; set; }
    public string Name { get; set; }

    public VolunteerStudyInformation VolunteerStudyInformation { get; set; }
    
    public ICollection<VolunteerStudyInformationGroupCriteria> Criteria { get; } = new List<VolunteerStudyInformationGroupCriteria>();
}