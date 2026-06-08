using BPOR.Domain.Enums;

namespace BPOR.Rms.VolunteerInformation.Models;

public class VsiGroupCriteriaModel
{
    public int Id { get; set; }
    public string Criteria { get; set; }
    public VolunteerStudyInformationGroupCriteriaTypeId Type { get; set; }
}