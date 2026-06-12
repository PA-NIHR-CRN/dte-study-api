using System.Text.RegularExpressions;

namespace NIHR.Infrastructure.AspNetCore;

public record Postcode
{
    public string Area { get; }
    public string District { get; }
    public string Sector { get; }
    public string Unit { get; }
    
    public string OutCode => $"{Area}{District}";
    
    public string InCode => $"{Sector}{Unit}";
    
    private static Regex _regex = new  Regex("([A-Z]{1,2})(\\d{1,2})(\\d)([A-Z]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private Postcode(string area, string district, string sector, string unit)
    {
        Area = area.ToUpper();
        District = district;
        Sector = sector;
        Unit = unit.ToUpper();
    }

    public static Postcode Create(string area, string district, string sector, string unit)
    {
        if (string.IsNullOrEmpty(area) || area.Length > 2 || !area.All(char.IsLetter))
        {
            throw new ArgumentException("Must be either 1 or 2 letters",  nameof(area));
        }
        
        if (string.IsNullOrEmpty(district) || district.Length > 2 || !area.All(char.IsDigit))
        {
            throw new ArgumentException("Must be either 1 or 2 digits",  nameof(district));
        }
        
        if (sector == null || sector.Length != 1 || !char.IsDigit(sector[0]))
        {
            throw new ArgumentException("Must be exactly 1 digit",  nameof(sector));
        }

        if (unit == null || unit.Length != 2 || !unit.All(char.IsLetter))
        {
            throw new ArgumentException("Must be exactly 2 letters", nameof(unit));
        }

        return new Postcode(area, district, sector, unit);
    }

    public override string ToString()
    {
        return $"{Area}{District} {Sector}{Unit}";
    }

    public static Postcode Parse(string postcodeString)
    {
        ArgumentNullException.ThrowIfNull(postcodeString);
        
        if (!TryParse(postcodeString, out var result))
        {
            throw new FormatException("String is not a valid postcode");
        }

        return result;
    }

    public static bool TryParse(string postcodeString, out Postcode result)
    {
        ArgumentNullException.ThrowIfNull(postcodeString);

        if (string.IsNullOrWhiteSpace(postcodeString))
        {
            result = null;
            return false;
        }
        
        var match = _regex.Match(postcodeString.RemoveWhitespace());
        if (!match.Success)
        {
            result = null;
            return false;     
        }

        result = new Postcode(
            match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value);
        return true;
    }
}