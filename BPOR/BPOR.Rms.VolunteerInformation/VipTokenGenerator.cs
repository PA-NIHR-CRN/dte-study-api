using System.Buffers.Binary;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace BPOR.Rms.VolunteerInformation;

internal class VipTokenGenerator(IOptions<RrvTokenOptions> options) : IVipTokenGenerator
{
    private readonly byte[] _preamble = [0x68, 0x3c, 0xb8, 0xac, 0x49];
    
    public string GenerateToken(VipTokenPurpose purpose, long id, string ivHexString) 
        => GenerateToken(purpose, id, Convert.FromHexString(ivHexString));

    public string GenerateToken(VipTokenPurpose purpose, long id) 
        => GenerateToken(purpose, id, GenerateIv());

    public string GenerateToken(VipTokenPurpose purpose, long id, byte[] iv)
    {
        Span<byte> payload = new byte[15];
        
        _preamble.CopyTo(payload.Slice(0, 5));
        payload[5] = 0x00; // version
        payload[6] = (byte)purpose; // purpose
        BinaryPrimitives.WriteInt64BigEndian(payload.Slice(7, 8), id);

        Span<byte> result = new byte[32];
        iv.CopyTo(result.Slice(0, 16));
        
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Convert.FromHexString(options.Value.SymmetricKey);
            aesAlg.IV = iv;
            aesAlg.Mode = CipherMode.CBC;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(payload);
                }

                var cipherBytes = msEncrypt.ToArray();
                cipherBytes.CopyTo(result.Slice(16, 16));
            }
        };
        
        return Convert.ToHexString(result);
    }

    public bool TryValidateToken(string token, out (VipTokenPurpose purpose, long Id) result)
    {
        Span<byte> rawBytes = Convert.FromHexString(token);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Convert.FromHexString(options.Value.SymmetricKey);
            aesAlg.IV = rawBytes.Slice(0, 16).ToArray();

            // Create an encryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor();

            Span<byte> plainBytes = new byte[16];

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream(rawBytes.Slice(16, 16).ToArray()))
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
                {
                    var bytesRead = csEncrypt.Read(plainBytes);
                    Debug.Assert(bytesRead == 15);
                }
            }

            if (!_preamble.SequenceEqual(plainBytes.Slice(0, 5).ToArray()))
            {
                result = default;
                return false;
            }

            var version = plainBytes[5];
            if (version != 0x00)
            {
                result = default;
                return false;
            }

            var purpose = (VipTokenPurpose)plainBytes[6];
            var id = BinaryPrimitives.ReadInt64BigEndian(plainBytes.Slice(7, 8));

            result = new(purpose, id);
            return true;
        }
    }
    
    public string GenerateIvString()
    {
        return Convert.ToHexString(GenerateIv());
    }

    private byte[] GenerateIv()
    {
        return RandomNumberGenerator.GetBytes(16);
    }
}