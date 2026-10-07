using BPOR.Domain.Enums;

namespace BPOR.Rms.PipelineTrackerIngest.TypeConvertors;

public class NihrFundingStatusTypeConvertor : EnumTypeConverter<NihrFundingStatusType>
{
    public override NihrFundingStatusType? ConvertFromString(string? text)
    {
        return text switch
        {
            "Yes" => NihrFundingStatusType.Yes,
            "No" => NihrFundingStatusType.No,
            "Under discussion" => NihrFundingStatusType.NoButApplied,
            "" => null,
            _ => throw new ArgumentException($"Unsupported study status {text}", nameof(text))
        };
    }
}