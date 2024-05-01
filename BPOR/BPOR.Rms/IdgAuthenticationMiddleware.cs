using BPOR.Domain.Entities;
using BPOR.Rms.Startup;
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
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        
        ArgumentNullException.ThrowIfNull(currentUserIdAccessor, nameof(currentUserIdAccessor));


        var token = context.RequestAborted;

        var userId = context.User.GetId();
        var isAuthenticated = context?.User?.Identity?.IsAuthenticated ?? false;

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