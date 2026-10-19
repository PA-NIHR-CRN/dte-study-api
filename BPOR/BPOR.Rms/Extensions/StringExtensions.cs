namespace BPOR.Rms.Extensions;

public static class StringExtensions
{
    public static string ToSentenceCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        value = value.ToLower();

        return char.ToUpper(value[0]) + value[1..];
    }
}