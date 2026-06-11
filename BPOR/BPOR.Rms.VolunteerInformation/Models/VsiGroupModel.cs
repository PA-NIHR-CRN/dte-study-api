namespace BPOR.Rms.VolunteerInformation.Models;

public class VsiGroupModel
{
    public int Id { get; set; }
    public int VolunteerStudyInformationId { get; set; }
    public string Name { get; set; }
    public IEnumerable<VsiGroupCriteriaModel> Criteria { get; set; } = [];
}