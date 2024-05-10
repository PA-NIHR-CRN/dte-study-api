using BPOR.Domain.Entities;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure;
public class IdgAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public IdgAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ParticipantDbContext dbContext, ICurrentUserIdAccessor<int> currentUserIdAccessor, ICurrentUserProvider<User> userProvider)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        ArgumentNullException.ThrowIfNull(dbContext);
        
        ArgumentNullException.ThrowIfNull(currentUserIdAccessor);

        if (context.Request.Query.ContainsKey("sign-out"))
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Logout functionality will not log the user out of IDG; similar to other IDG apps in the NIHR, you will still be logged in to other tools pending session expiry
            // await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            context.Response.Redirect("/");
        }

        var token = context.RequestAborted;

        var userId = context.User.GetId();
        var isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;

        if (isAuthenticated && !string.IsNullOrWhiteSpace(userId))
        {
            var user = await dbContext.User.SingleOrDefaultAsync(x => x.AuthenticationId == userId, token);

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
        }

        await _next(context);
    }
}