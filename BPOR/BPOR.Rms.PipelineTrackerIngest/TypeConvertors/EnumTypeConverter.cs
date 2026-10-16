using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace BPOR.Rms.PipelineTrackerIngest.TypeConvertors;

public class EnumTypeConverter<TEnum> : DefaultTypeConverter
    where TEnum : struct, Enum
{
    public sealed override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return ConvertFromString(text);
    }

    public virtual TEnum? ConvertFromString(string? text)
    {
        return text == null 
            ? throw new ArgumentException($"'{text}' could not be converted to {typeof(TEnum).Name}")
            : Enum.Parse<TEnum>(text);
    }
}