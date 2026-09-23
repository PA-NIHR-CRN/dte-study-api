using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken; 

public static class AccessTokenExtensions
{
    private const string ConfigSectionPath = "AccessTokenAuthentication";

    public static AuthenticationBuilder AddAccessTokenAuthentication(this AuthenticationBuilder builder,
        string? scheme = null)
    {
        builder.Services.AddScoped<IAuthorizationHandler, AccessTokenRequirementHandler>();
        builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
        builder.Services.AddScoped<IUrlAccessTokenService, UrlAccessTokenService>();
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
    
    public static bool HasValidAccessToken(this IAccessTokenService accessTokenService, HttpContext httpContext, string role)
    {
        return httpContext.User.Claims
            .Where(i => 
                i.Subject?.AuthenticationType == AccessTokenAuthenticationOptions.AuthenticationScheme &&
                i.Type == AccessTokenAuthenticationOptions.ClaimType)
            .Select(claim => accessTokenService.DeserializeClaim(claim.Value))
            .Any(token => string.Equals(token.Role, role) && IsAuthorizedRoute(token, httpContext.Request.RouteValues));
    }
    
    private static bool IsAuthorizedRoute(AccessToken token, RouteValueDictionary route)
    {
        foreach (var requiredRoute in token.RouteValues)
        {
            if (!route.TryGetValue(requiredRoute.Key, out var actualRouteValue) ||
                !string.Equals(actualRouteValue, requiredRoute.Value))
            {
                return false;
            }
        }

        return true;
    }
}