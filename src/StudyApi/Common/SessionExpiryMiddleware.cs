using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Extensions;
using AWS.Lambda.Powertools.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StudyApi.Common;

public class SessionExpiryMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SessionExpiryMiddleware> _logger;

    public SessionExpiryMiddleware(RequestDelegate next, ILogger<SessionExpiryMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!result.Succeeded)
        {
            if (result.Failure is not null)
            {
                _logger.LogWarning(result.Failure, "Cookie authentication failed. {@message}", result.Failure.Message);
            }
            else
            {
                _logger.LogWarning("Cookie authentication failed.");
            }

            await _next(context);
            return;
        }

        var sessionService = context.RequestServices.GetRequiredService<ISessionService>();
        var sessionId = result.Principal.FindFirstValue("sessionId");
        var participantId = context.User.GetParticipantId();

        var scopeData = new Dictionary<string, string>
        {
            ["sessionId"] = sessionId,
            ["participantId"] = participantId
        };

        _logger.AppendKeys(scopeData);

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