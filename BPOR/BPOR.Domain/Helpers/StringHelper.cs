namespace BPOR.Domain;

public static class StringHelper
{
    public static string RemoveWhitespace(this string input)
    {
        ArgumentNullException.ThrowIfNull(input);
        
        return new string(input
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());
    }
}