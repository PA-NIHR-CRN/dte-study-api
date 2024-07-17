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
        for (var i = 1; i < 16; i++)
        {
            reference.Append(_random.Next(0, 10));
            if (i % 4 == 0)
            {
                reference.Append(' ');
            }
        }

        var checkDigit = Luhn.CalculateCheckDigit(reference.ToString());
        reference.Append(checkDigit);

        return reference.ToString();
    }
}
