using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace BPOR.Rms.PipelineTrackerIngest.TypeConvertors;

public class YesNoConverter : DefaultTypeConverter
{
    public sealed override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return text switch
        {
            "" => null,
            null => null,
            "Yes" => true,
            "No" => false
        };
    }
}