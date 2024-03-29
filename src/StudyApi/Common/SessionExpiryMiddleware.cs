using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace StudyApi.Common;

public class SessionExpiryMiddleware
{
    private readonly RequestDelegate _next;

    public SessionExpiryMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceScopeFactory serviceScopeFactory)
    {
        var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!result.Succeeded)
        {
            await _next(context);
            return;
        }

        using var scope = serviceScopeFactory.CreateScope();
        var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
        var sessionId = result.Principal.FindFirstValue("sessionId");
        var participantId = context.User.GetParticipantId();

        var token = result.Principal.Claims;
        var nhsNumber = token?.FirstOrDefault(x => x.Type == "nhs_number")?.Value;

        if (nhsNumber == null)
        {
            var isSessionValid = await sessionService.ValidateSessionAsync(participantId, sessionId);

            if (!isSessionValid)
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                context.Response.Cookies.Delete(".BPOR.Session.Expiry");
            }
        }

        // Call the next delegate/middleware in the pipeline
        await _next(context);
    }
}