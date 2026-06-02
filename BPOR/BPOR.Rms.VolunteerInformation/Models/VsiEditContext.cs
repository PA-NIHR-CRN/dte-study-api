using BPOR.Domain.Entities;

namespace BPOR.Rms.VolunteerInformation.Models;

public class VsiEditContext
{
    public required VolunteerStudyInformation Vsi  { get; init; }
    public string? BackUrl { get; init; }
    public required string SectionName { get; init; }
}