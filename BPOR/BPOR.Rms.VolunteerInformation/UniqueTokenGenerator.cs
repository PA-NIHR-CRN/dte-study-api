using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography;

namespace BPOR.Rms.VolunteerInformation;

public static class UniqueTokenGenerator
{
        const int bitsPerByte = 8;
        
    /// <summary>
    /// Creates a pseudo-random 32 character token such that two tokens generated for different seed values are guaranteed to differ.
    /// </summary>
    /// <remarks>
    /// The seed value can easily be determined from the token - this function is not intended to protect the seed.
    ///
    /// This is not meant to be massively optimised.
    /// </remarks>
    public static string CreateUniqueToken(long seed)
    {
        const int resultByteCount = 32;
        
        int[] seedMap =
        [
            208, 53, 86, 227, 29, 10, 120, 199, 18, 112, 1, 214, 186, 99, 130, 219, 56, 251, 224, 169, 48, 138, 167,
            203, 15, 67, 91, 196, 119, 57, 180, 133, 135, 5, 142, 206, 84, 83, 73, 42, 211, 64, 163, 158, 108, 175, 118,
            95, 127, 179, 13, 43, 221, 76, 51, 65, 223, 19, 239, 217, 88, 193, 153, 96
        ];
        
        Debug.Assert(seedMap.Length == sizeof(long) * bitsPerByte);

        byte[] result = RandomNumberGenerator.GetBytes(resultByteCount);

        var seedBytes = BitConverter.GetBytes(seed);
        for (int i = 0; i < seedMap.Length; i++)
        {
            SetBit(result, seedMap[i], GetBit(seedBytes, i));
        }

        // Bitwise chained xor to 'hide' the seed bits....
        bool previousBit = GetBit(result, 0);
        for (int i = 1; i < resultByteCount * bitsPerByte; i++)
        {
            bool currentBit = GetBit(result, i);
            SetBit(result, i, previousBit ^ currentBit);
            previousBit = currentBit;
        }

        return Convert.ToHexString(result);
    }

    private static bool GetBit(byte[] target, int bitIndex)
    {
        byte mask = (byte)(1 << (bitIndex % bitsPerByte));
        var maskedResult = target[bitIndex / bitsPerByte] & mask;
        return maskedResult != 0;
    }

    private static void SetBit(byte[] target, int bitIndex, bool value)
    {
        byte mask = (byte)(1 << (bitIndex % bitsPerByte));
        if (value)
        {
            target[bitIndex / bitsPerByte] |= mask;
        }
        else
        {
            target[bitIndex / bitsPerByte] &= (byte)~mask;
        }
    }
}