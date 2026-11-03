using BPOR.Domain.Enums;

namespace BPOR.Rms.Models.Study;

public sealed class StudyFilterViewModel
{
    public HashSet<StudyStatusType> SelectedStatuses { get; set; } = null!;
}