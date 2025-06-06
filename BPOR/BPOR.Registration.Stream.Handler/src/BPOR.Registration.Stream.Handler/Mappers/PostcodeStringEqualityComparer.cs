using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BPOR.Registration.Stream.Handler.Mappers;

/// <summary>
/// Compares strings for equalily ignoring whitespace and case; treats null and empty string as equal.
/// </summary>
public class PostcodeStringEqualityComparer : IEqualityComparer<string>
{
    public bool Equals(string? x, string? y)
    {
        bool xIsNullOrWhitespace = string.IsNullOrWhiteSpace(x);
        bool yIsNullOrWhitespace = string.IsNullOrWhiteSpace(y);

        if (xIsNullOrWhitespace && yIsNullOrWhitespace) 
            return true;
        if (xIsNullOrWhitespace || yIsNullOrWhitespace)
            return false;

        return string.Equals(Normalise(x!), Normalise(y!));
    }

    private static string Normalise(string postcode)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in postcode) 
        {
            if (!char.IsWhiteSpace(c))
            {
                result.Append(char.ToUpper(c));
            }
        }
        return result.ToString();
    }

    public int GetHashCode([DisallowNull] string obj)
    {
        return Normalise(obj).GetHashCode();
    }
}
