using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BPOR.Infrastructure.Authentication;

public static class DiExtensions
{
    public static AuthenticationBuilder AddApiKeyAuthentication(this AuthenticationBuilder builder,
        string? scheme = null)
    {
        builder.Services.AddOptions<ApiKeyAuthenticationOptions>().BindConfiguration("ApiKeyAuthentication");
        return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
            scheme ?? ApiKeyAuthenticationOptions.DefaultScheme, _ => { });
    }
}