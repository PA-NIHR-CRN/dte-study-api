using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public class AccessTokenService(IDataProtectionProvider dataProtectionProvider, 
    IOptions<AccessTokenAuthenticationOptions> options) : IAccessTokenService
{
    private const string protectionPurpose = "AccessToken";
    
    public string EncryptAccessToken(AccessToken token, TimeSpan? lifetime = null)
    {
        string json = JsonSerializer.Serialize(token);
        return CreateProtector().Protect(json, lifetime ?? options.Value.TokenLifetime);
    }

    private ITimeLimitedDataProtector CreateProtector()
    {
        return dataProtectionProvider.CreateProtector(protectionPurpose).ToTimeLimitedDataProtector();
    }

    public string DecryptAccessToken(string token)
    {
        return CreateProtector().Unprotect(token);
    }

    public AccessToken DeserializeClaim(string claim)
    {
        return JsonSerializer.Deserialize<AccessToken>(claim);   
    }
}