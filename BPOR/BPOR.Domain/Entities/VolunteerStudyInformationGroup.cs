namespace BPOR.Domain.Entities;

public class VolunteerStudyInformationGroup
{
    public int Id { get; set; }
    public int VolunteerStudyInformationId { get; set; }
    public string Name { get; set; }

    public VolunteerStudyInformation VolunteerStudyInformation { get; set; }
    
    public ICollection<VolunteerStudyInformationGroupCriteria> Criteria { get; set; } = new List<VolunteerStudyInformationGroupCriteria>();
}