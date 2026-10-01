using BPOR.Domain.Enums;

namespace BPOR.Rms.Models.Study;

public sealed class StudyStatusFilterOptionViewModel
{
    public StudyStatusType StatusType { get; init; }
    public int Value { get; init; }

    public string Text { get; init; } = string.Empty;

    public bool IsSelected { get; init; }
}