using BPOR.Domain.Entities;
using BPOR.Rms.Startup;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure;
using NIHR.Infrastructure.Settings;
using System.Security.Claims;
public class IdgAuthenticationMiddleware
{
    private const string AccountNotRegisteredPath = "/Account/NotRegistered";
    private const string ReturnUrlParameterName = "ReturnUrl";
    private readonly RequestDelegate _next;

    public IdgAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ParticipantDbContext dbContext, ICurrentUserIdAccessor<int> currentUserIdAccessor, ICurrentUserProvider<User> userProvider, IOptions<AuthenticationSettings> authenticationOptions)
    {
        ArgumentNullException.ThrowIfNull(context);

        ArgumentNullException.ThrowIfNull(dbContext);

        ArgumentNullException.ThrowIfNull(currentUserIdAccessor);

        if (context.Request.Query.ContainsKey("sign-out"))
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Logout functionality will not log the user out of IDG; similar to other IDG apps in the NIHR, you will still be logged in to other tools pending session expiry
            // await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            context.Response.Redirect("/Account/SignedOut");
        }

        var token = context.RequestAborted;


        if (authenticationOptions.Value.Bypass)
        {
            var identity = new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, authenticationOptions.Value.BypassUserId)], "Bypass");
            context.User = new ClaimsPrincipal(identity);
        }

        var userId = context.User.GetId();
        var isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;

        if (isAuthenticated && !string.IsNullOrWhiteSpace(userId))
        {
            // TODO: make this more efficient without eager loading.
            var user = await dbContext
                .User
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                .SingleOrDefaultAsync(x => x.AuthenticationId == userId, token);

            if (user is null)
            {
                user = new User
                {
                    AuthenticationId = userId,
                    ContactEmail = context.User.GetEmail(),
                    ContactFullName = context.User.GetName() ?? context.User.GetEmail(),
                };

                dbContext.User.Add(user);

                await dbContext.SaveChangesAsync(token);
            }

            currentUserIdAccessor.SetCurrentUserId(user?.Id ?? 0);
            userProvider.User = user;

            var identity = context.User.Identity as ClaimsIdentity;
            if (identity != null && user != null)
            {
                // Add role claims to the user identity for use during authorization
                // throughout the application.
                foreach (var userRole in user.UserRoles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, userRole.Role.Code));
                }

                // User has no roles. Send them to the Account Not Registered page.
                if (!user.UserRoles.Any() && context.Request.Path != AccountNotRegisteredPath)
                {
                    context.Response.Redirect($"{AccountNotRegisteredPath}?{ReturnUrlParameterName}={Uri.EscapeDataString(context.Request.Path)}");
                    return;
                }

                // Request is for the Account Not Registered page, but user is registered
                // (possibly due to a stale browser history item) send them to their original
                // location or the home page.
                if (user.UserRoles.Any() && context.Request.Path == AccountNotRegisteredPath)
                {
                    context.Request.Query.TryGetValue(ReturnUrlParameterName, out var returnUrl);
                    context.Response.Redirect(returnUrl.FirstOrDefault() ?? "/");

                    return;
                }
            }
        }

        await _next(context);
    }
}