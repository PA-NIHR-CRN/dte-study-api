using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BPOR.Infrastructure.Authentication;

public sealed class ApiKeyAuthenticationHandler(
    IOptionsMonitor<ApiKeyAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IConfiguration configuration)
    : AuthenticationHandler<ApiKeyAuthenticationOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(
                ApiKeyAuthenticationOptions.HeaderName, out var providedKey))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (string.IsNullOrEmpty(options.CurrentValue.ApiKey))
        {
            return Task.FromResult(
                AuthenticateResult.Fail("API key is not configured on the server."));
        }

        var providedParts = providedKey.ToString().Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);

        if (providedParts.Length != 2 ||
            !providedParts[0].Equals("bearer", StringComparison.InvariantCultureIgnoreCase))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        
        var providedBytes = Encoding.UTF8.GetBytes(providedParts[1]);
        var expectedBytes = Encoding.UTF8.GetBytes(options.CurrentValue.ApiKey);

        if (providedBytes.Length != expectedBytes.Length ||
            !CryptographicOperations.FixedTimeEquals(providedBytes, expectedBytes))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key."));
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "static-client"),
            new Claim("client_id", "static-client")
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}