using System.Text;
using BPOR.Rms.Utilities.Interfaces;
using LuhnNet;

namespace BPOR.Rms.Utilities;

public class ReferenceGenerator : IReferenceGenerator
{
    private static readonly Random _random = new();
    
    public string GenerateReference()
    {
        var reference = new StringBuilder();
        for (var i = 0; i < 15; i++)
        {
            reference.Append(_random.Next(0, 10));
        }

        var checkDigit = Luhn.CalculateCheckDigit(reference.ToString());
        reference.Append(checkDigit);

        return reference.ToString();
    }
}
