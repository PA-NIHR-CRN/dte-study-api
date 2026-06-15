using System.Security.Cryptography;
using BPOR.Rms.Abstractions.Enums;
using Microsoft.AspNetCore.DataProtection;

namespace BPOR.Rms.VolunteerInformation.Services;

public class InternalVipTokenService (IDataProtectionProvider dataProtectionProvider)
{
    private const string Preamble = "00-";

    public string CreateInternalVipAccessToken(VolunteerInformationAudience audience, int studyId)
    {
        string plainText = $"{audience}:{studyId}";
        string cipherText = CreateDataProtector().Protect(plainText, DateTimeOffset.UtcNow.AddHours(1));
        return Preamble + cipherText;
    }

    private ITimeLimitedDataProtector CreateDataProtector()
        => dataProtectionProvider.CreateProtector(nameof(InternalVipTokenService)).ToTimeLimitedDataProtector();

    public bool TryValidateVipAccessToken(string token, out (VolunteerInformationAudience audience, int studyId) value)
    {
        try
        {
            if (!token.StartsWith(Preamble))
            {
                value = default;
                return false;
            }
            
            var plainText = CreateDataProtector().Unprotect(token.Substring(Preamble.Length));
            var parts = plainText.Split(':', 2);
            if (parts.Length != 2 || !Enum.TryParse<VolunteerInformationAudience>(parts[0], out var audience) || !int.TryParse(parts[1], out var studyId))
            {
                value = default;
                return false;
            }
            
            value = (audience, studyId);
            return true;
        }
        catch (CryptographicException)
        {
            value = default;
            return false;
        }
    }
}