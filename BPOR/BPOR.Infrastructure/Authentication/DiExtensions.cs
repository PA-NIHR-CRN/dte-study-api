using Microsoft.AspNetCore.Authentication;

namespace BPOR.Infrastructure.Authentication;

public static class DiExtensions
{
    public static AuthenticationBuilder AddApiKeyAuthentication(this AuthenticationBuilder builder,
        string? scheme = null)
    {
        return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
            scheme ?? ApiKeyAuthenticationOptions.DefaultScheme, _ => { });
    }
}