using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.VolunteerInformation.Models;

public class StartModel
{
    public int StudyId { get; set; }
    public VsiStatus? Status  { get; set; }
}

