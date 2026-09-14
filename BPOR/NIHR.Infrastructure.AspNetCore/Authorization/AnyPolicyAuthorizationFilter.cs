using CpmsCore.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NIHR.Infrastructure.AspNetCore.Authorization;

public class AnyPolicyAuthorizationFilter(IAuthorizationService authorizationService) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        foreach (AuthorizeAnyPolicyAttribute att in context.ActionDescriptor.EndpointMetadata
                     .OfType<AuthorizeAnyPolicyAttribute>())
        {
            bool result = false;
            foreach (string policy in att.Policies)
            {
                result = (await authorizationService.AuthorizeAsync(context.HttpContext.User, context.HttpContext, policy)).Succeeded;
                if (result)
                {
                    break;
                }
            }

            if (!result)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
