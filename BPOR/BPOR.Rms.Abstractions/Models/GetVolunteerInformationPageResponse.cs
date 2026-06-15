using BPOR.Rms.Abstractions.Entities;
using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.Abstractions.Models;

public class GetVolunteerInformationPageResponse
{
    public VolunteerInformationAudience Audience { get; set; }
    public required VsiPage VolunteerInformation { get; set; }
    public string? FullPrescreenerLink { get; set; }
}