using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;

public interface IApiKeyClaimProvider
{
    bool TryGetClaims(string apiKey, [MaybeNullWhen(false)] out Claim[] claims);
}