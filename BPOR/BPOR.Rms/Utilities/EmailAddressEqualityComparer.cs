namespace BPOR.Rms.Utilities;

public class EmailAddressEqualityComparer : IEqualityComparer<string>
{
    public bool Equals(string? x, string? y)
    {
        if (x == null && y == null)
            return true;
        
        if (x == null || y == null)
            return false;
        
        return string.Equals(
            x.Trim(), 
            y.Trim(), 
            StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(string obj)
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Trim());
    }
}