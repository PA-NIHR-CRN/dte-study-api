using BPOR.Domain.Enums;

namespace BPOR.Rms.VolunteerInformation.Models;

public class CreateCriterionModel : CreateCriterionPostbackModel
{
    public string GroupName { get; set; }
    public VolunteerStudyInformationGroupCriteriaTypeId Type { get; set; }
}