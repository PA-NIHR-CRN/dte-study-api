using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;

public sealed class ApiKeyAuthenticationHandler(
    IOptionsMonitor<ApiKeyAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IEnumerable<IApiKeyClaimProvider> claimProviders)
    : AuthenticationHandler<ApiKeyAuthenticationOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(
                ApiKeyAuthenticationOptions.HeaderName, out var providedKey))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }


        var providedParts = providedKey.ToString().Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);

        if (providedParts.Length != 2 ||
            !providedParts[0].Equals("bearer", StringComparison.InvariantCultureIgnoreCase))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        
        foreach (var claimProvider in claimProviders)
        {
            if (claimProvider.TryGetClaims(providedParts[1], out var claims))
            {
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
        }

        return Task.FromResult(AuthenticateResult.Fail("Invalid API key."));
    }
}