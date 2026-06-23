using BPOR.Domain.Entities;
using BPOR.Rms.VolunteerInformation.Controllers;

namespace BPOR.Rms.VolunteerInformation.Models;

public class VsiEditContext
{
    public int StudyId { get; set; }
    public VipFlowMode FlowMode { get; set; }

    public virtual Dictionary<string, string> ToRouteData() =>
        new Dictionary<string, string>
        {
            [nameof(StudyId)] = StudyId.ToString(),
            [nameof(FlowMode)] = FlowMode.ToString()
        };
}