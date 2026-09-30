namespace BPOR.Rms.Utilities;

public class EmailAddressEqualityComparer 
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
}