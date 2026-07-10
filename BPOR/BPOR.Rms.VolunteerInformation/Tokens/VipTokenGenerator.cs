using BPOR.Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using ProtoBuf;

namespace BPOR.Rms.VolunteerInformation.Tokens;

internal class VipTokenGenerator(IDataProtectionProvider dataProtectionProvider, ParticipantDbContext db)
    : IVipTokenGenerator
{
    private const string _protectionPurpose = "VipAccess";
    private const byte _version = 0x00;
    
    public async Task<VipToken?> ValidateToken(string tokenString, CancellationToken cancellationToken)
    {
        return TryValidateSelfContainedToken(tokenString, out var result) 
            ? result
            : null;
    }
    
    public string GenerateToken(VipToken token)
    {
        using MemoryStream stream = new MemoryStream();
        stream.Write([_version]);
        Serializer.Serialize(stream, token);
        byte[] cipherBytes = dataProtectionProvider
            .CreateProtector(_protectionPurpose).Protect(stream.ToArray());
        return Convert.ToHexString(cipherBytes);
    }

    public bool TryValidateSelfContainedToken(string token, out VipToken result)
    {
        try
        {
            var cipherBytes = Convert.FromHexString(token);
            Span<byte> plainBytes = dataProtectionProvider
                .CreateProtector(_protectionPurpose).Unprotect(cipherBytes);
            if (plainBytes.Length < 1)
            {
                throw new ArgumentException("Token too short");
            }
            if (plainBytes[0] != _version)
            {
                throw new ArgumentException("Invalid token version");
            }
            result = Serializer.Deserialize<VipToken>(plainBytes[1..]);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}