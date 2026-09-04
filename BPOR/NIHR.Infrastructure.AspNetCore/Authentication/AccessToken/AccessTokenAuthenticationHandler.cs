using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public sealed class AccessTokenAuthenticationHandler(
    IOptionsMonitor<AccessTokenAuthenticationOptions> options,
    ILoggerFactory logger,
    IAccessTokenService accessTokenService,
    UrlEncoder encoder)
    : AuthenticationHandler<AccessTokenAuthenticationOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var accessTokenValues = Request.Query[options.CurrentValue.QueryParameterName];
        if (accessTokenValues.Count != 1)
        {
            return AuthenticateResult.NoResult();
        }

        string token;
        try
        {
            token = accessTokenService.DecryptAccessToken(accessTokenValues.First() ?? string.Empty);
        }
        catch (CryptographicException)
        {
            Context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await Context.Response.WriteAsync("Authentication blocked.");
            return AuthenticateResult.Fail("Invalid access token");
        }

        Claim[] claims = [new (AccessTokenAuthenticationOptions.ClaimType, token)];
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}