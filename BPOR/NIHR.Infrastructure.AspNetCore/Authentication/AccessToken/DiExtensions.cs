using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken; 

public static class AccessTokenDiExtensions
{
    private const string ConfigSectionPath = "AccessTokenAuthentication";

    public static AuthenticationBuilder AddAccessTokenAuthentication(this AuthenticationBuilder builder,
        string? scheme = null)
    {
        builder.Services.AddScoped<IAuthorizationHandler, AccessTokenRequirementHandler>();
        builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
        builder.Services.AddOptions<AccessTokenAuthenticationOptions>().BindConfiguration(ConfigSectionPath);
        return builder.AddScheme<AccessTokenAuthenticationOptions, AccessTokenAuthenticationHandler>(
            scheme ?? AccessTokenAuthenticationOptions.AuthenticationScheme, _ => { });
    }

    public static void AddAccessTokenPolicy(this AuthorizationOptions options,
        string policyName, string accessTokenRoleName)
    {
        options.AddPolicy(policyName, policy =>
        {
            policy.AuthenticationSchemes.Add(AccessTokenAuthenticationOptions.AuthenticationScheme);
            policy.Requirements.Add(new AccessTokenRequirement(accessTokenRoleName));
        });
    }
}