using System.Collections;
using System.Security.Cryptography;

namespace BPOR.Rms.VolunteerInformation;

public static class UniqueTokenGenerator
{
    /// <summary>
    /// Creates a 32 character token such that two tokens generated for different seed values are guaranteed to be unique.
    /// </summary>
    /// <remarks>
    /// The seed value can easily be determined from the token - this function is not intended to protect the seed.
    /// </remarks>
    public static string CreateUniqueToken(int seed)
    {
        int[] seedMap = [ 20, 68, 69, 22, 120, 6, 18, 61, 117, 59, 110, 54, 78, 46, 111, 108, 81, 0, 82, 55, 25, 43, 24, 102, 32, 70, 115, 26, 2, 63, 125, 84];
        
        byte[] result = RandomNumberGenerator.GetBytes(16);

        var seedBytes = BitConverter.GetBytes(seed);
        for (int i = 0; i < 32; i++)
        {
            SetBit(result, seedMap[i], GetBit(seedBytes, i));
        }
        return Convert.ToHexString(result);
    }
    
    private static bool GetBit(byte[] target, int bitIndex)
    {
        byte mask = (byte)(1 << (bitIndex % 8));
        var maskedResult = target[bitIndex / 8] & mask;
        return maskedResult != 0;
    }

    private static void SetBit(byte[] target, int bitIndex, bool value)
    {
        byte mask = (byte)(1 << (bitIndex % 8));
        if (value)
        {
            target[bitIndex / 8] |= mask;
        }
        else
        {
            target[bitIndex / 8] &= (byte)~mask;
        }
    }
}