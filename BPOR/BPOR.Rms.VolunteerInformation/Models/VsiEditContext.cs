using BPOR.Domain.Entities;

namespace BPOR.Rms.VolunteerInformation.Models;

public class VsiEditContext
{
    public VolunteerStudyInformation Vsi  { get; set; }
    public string? SkipUrl { get; set; }
    public string? SectionName { get; set; }
    public int StudyId => Vsi.StudyId;
}