using System.Text;
using BPOR.Rms.Utilities.Interfaces;
using LuhnNet;

namespace BPOR.Rms.Utilities;

public class ReferenceGenerator : IReferenceGenerator
{
    private static readonly Random _random = new();
    
    public string GenerateReference()
    {
        var reference = new StringBuilder(16);
        Span<int> span = stackalloc int[15];
        for (var i = 0; i < span.Length; i++)
        {
            span[i] = _random.Next(0, 10);
            reference.Append(span[i]);
        }

        reference.Append(Luhn.CalculateCheckDigit(reference.ToString()));
        return reference.ToString();
    }
}
