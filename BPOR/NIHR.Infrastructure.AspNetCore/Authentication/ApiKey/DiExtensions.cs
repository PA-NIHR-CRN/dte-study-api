using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;

public static class DiExtensions
{
    public static AuthenticationBuilder AddApiKeyAuthentication(this AuthenticationBuilder builder,
        string? scheme = null)
    {
        builder.Services.AddOptions<ApiKeyAuthenticationOptions>().BindConfiguration("ApiKeyAuthentication");
        builder.Services.AddScoped<IApiKeyClaimProvider, ConfigurationApiKeyClaimsProvider>();
        return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
            scheme ?? ApiKeyAuthenticationOptions.DefaultScheme, _ => { });
    }

    public static IServiceCollection AddApiKeyRoleFromOptions<TOptions>(this IServiceCollection services,
        Func<TOptions, string> keySelector, string roleName) where TOptions : class
    {
        return services.AddSingleton<IApiKeyClaimProvider>(services =>
        {
            var options = services.GetRequiredService<IOptions<TOptions>>();
            var fixedApiKey = keySelector(options.Value);
            return new FixedApiKeyRole(fixedApiKey, roleName);
        });
    }
}

public class FixedApiKeyRole (string fixedApiKey, string roleName) : IApiKeyClaimProvider
{
    public bool TryGetClaims(string apiKey, [MaybeNullWhen(false)] out Claim[] claims)
    {
        if (apiKey != fixedApiKey)
        {
            claims = null;
            return false;
        }
        
        claims = [new Claim(ClaimTypes.Role, roleName)];
        return true;
    }
}