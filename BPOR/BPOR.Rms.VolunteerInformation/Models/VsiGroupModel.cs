using BPOR.Domain.Enums;

namespace BPOR.Rms.VolunteerInformation.Models;

public class VsiGroupModel
{
    public int Id { get; set; }
    public int VolunteerStudyInformationId { get; set; }
    public string Name { get; set; }
    public List<VsiGroupCriteriaModel> Criteria { get; set; } = new();
    public int StudyId { get; set; }
}

public class VsiGroupCriteriaModel
{
    public int Id { get; set; }
    public string Criteria { get; set; }
    public VolunteerStudyInformationGroupCriteriaTypeId Type { get; set; }
}