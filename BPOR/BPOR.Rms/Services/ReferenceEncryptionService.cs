using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.WebUtilities;
using NIHR.Infrastructure.Interfaces;

namespace BPOR.Rms.Services;

public class ReferenceEncryptionService(IDataProtectionProvider dataProtectionProvider) : IEncryptionService
{
    private readonly IDataProtector _protector = dataProtectionProvider.CreateProtector("ReferenceProtector");

    public string Encrypt(string input)
    {
        var protectedBytes = _protector.Protect(Encoding.UTF8.GetBytes(input));
        return WebEncoders.Base64UrlEncode(protectedBytes);
    }

    public string Decrypt(string input)
    {
        var protectedBytes = WebEncoders.Base64UrlDecode(input);
        var plainBytes = _protector.Unprotect(protectedBytes);
        return Encoding.UTF8.GetString(plainBytes);
    }
}
