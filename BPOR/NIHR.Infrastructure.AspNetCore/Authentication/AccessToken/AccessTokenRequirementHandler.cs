using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public class AccessTokenRequirementHandler(IAccessTokenService accessTokenService)
    : AuthorizationHandler<AccessTokenRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, AccessTokenRequirement requirement)
    {
        if (context.Resource is DefaultHttpContext defaultHttpContext)
        {
            foreach (var claim in context.User.Claims.Where(
                         i => i.Subject?.AuthenticationType == AccessTokenAuthenticationOptions.AuthenticationScheme && 
                              i.Type == AccessTokenAuthenticationOptions.ClaimType))
            {
                var token = accessTokenService.DeserializeClaim(claim.Value);
                if (string.Equals(token.Role, requirement.TokenRole) &&
                    IsAuthorizedRoute(token, defaultHttpContext.Request.RouteValues))
                {
                    context.Succeed(requirement);
                }
            }
        }

        return Task.CompletedTask;
    }

    private bool IsAuthorizedRoute(AccessToken token, RouteValueDictionary route)
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