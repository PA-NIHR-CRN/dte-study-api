using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;

public class ConfigurationApiKeyClaimsProvider(IOptions<ApiKeyAuthenticationOptions> options)
    : IApiKeyClaimProvider
{
    public bool TryGetClaims(string apiKey, [MaybeNullWhen(false)] out Claim[] claims)
    {
        var key = options.Value.FixedApiKeys.SingleOrDefault(i => i.ApiKey == apiKey);
        if (key == null)
        {
            claims = null;
            return false;
        }

        List<Claim> claimlist = new();
        if (key.Claims != null)
        {
            claimlist.AddRange(key.Claims.Select(i => new Claim(i.Type, i.Value)).ToArray());
        }
        if (key.Roles != null)
        {
            claimlist.AddRange(key.Roles.Select(i => new Claim(ClaimTypes.Role, i)).ToArray());
        }

        claims = claimlist.ToArray();
        return true;
    }
}