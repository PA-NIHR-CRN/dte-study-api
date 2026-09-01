using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public static class DiExtensions
{
    public static AuthenticationBuilder AddAccessTokenAuthentication(this AuthenticationBuilder builder,
        string? scheme = null)
    {
        builder.Services.AddScoped<IAuthorizationHandler, AccessTokenRequirementHandler>();
        builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
        builder.Services.AddOptions<AccessTokenAuthenticationOptions>().BindConfiguration("ApiKeyAuthentication");
        return builder.AddScheme<AccessTokenAuthenticationOptions, AccessTokenAuthenticationHandler>(
            scheme ?? AccessTokenAuthenticationOptions.DefaultScheme, _ => { });
    }
}