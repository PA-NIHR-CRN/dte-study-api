namespace BPOR.Rms.Utilities;

public class YesNoFormatter : IDisplayStringFormatter
{
    public string ToDisplayString(object? value)
        => value switch
        {
            true => "Yes",
            false => "No",
            _ => string.Empty
        };
}