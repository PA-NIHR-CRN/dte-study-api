using BPOR.Domain.Enums;

namespace BPOR.Rms.PipelineTrackerIngest.TypeConvertors;

public class StudyStatusTypeConvertor : EnumTypeConverter<StudyStatusType>
{
    public override StudyStatusType? ConvertFromString(string? text)
    {
        return text switch
        {
            "Submitted" => StudyStatusType.NewApplication,
            "In Progress" => StudyStatusType.InProgress,
            "Active (Pre-RMS)" => StudyStatusType.Active,
            "RMS-Active" => StudyStatusType.Active,
            "Concluded successfully" => StudyStatusType.ConcludedSuccessfully,
            "Rejected" => StudyStatusType.Rejected,
            "Withdrawn" => StudyStatusType.Withdrawn,
            "" => null,
            _ => throw new ArgumentException($"Unsupported study status {text}", nameof(text))
        };
    }
}