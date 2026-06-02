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

public class VsiContactModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Organisation { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
}