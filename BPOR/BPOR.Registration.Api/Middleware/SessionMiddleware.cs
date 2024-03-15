using System.Security.Claims;
using BPOR.Infrastructure.Interfaces;
using BPOR.Registration.Api.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BPOR.Registration.Api.Middleware;

public class SessionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IServiceScopeFactory serviceScopeFactory,
        CancellationToken cancellationToken = default)
    {
        var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!result.Succeeded)
        {
            await next(context);
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
            var isSessionValid = await sessionService.ValidateSessionAsync(participantId, sessionId, cancellationToken);

            if (!isSessionValid)
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                context.Response.Cookies.Delete(".BPOR.Session.Expiry");
            }
        }

        await next(context);
    }
}
