namespace NIHR.Infrastructure.AspNetCore;

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