using System.Text.RegularExpressions;
using BPOR.Rms.Utilities.Interfaces;
using Rbec.Postcodes;

namespace BPOR.Rms.Utilities;

public class PostcodeValidator : IPostcodeValidator
{
    public bool IsValidUkPostcode(Postcode? postcode)
    {
        if (string.IsNullOrEmpty(postcode.ToString()))
        {
            return false;
        }

        var regex = new Regex(@"^([Gg][Ii][Rr] 0[Aa]{2}|(?![Qq][Vv][Xx])[A-Za-z]{1,2}[0-9Rr][0-9A-Za-z]? ?[0-9][A-Za-z]{2})$");

        return regex.IsMatch(postcode.ToString());
    }
}
