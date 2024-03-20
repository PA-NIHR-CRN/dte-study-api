using BPOR.Domain.Settings;
using Microsoft.Extensions.Options;

namespace BPOR.Registration.Api.Middleware;

public class MaintenanceMiddleware(IOptionsMonitor<DevelopmentSettings> devSettings, RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (devSettings.CurrentValue.IsInMaintenance)
        {
            context.Response.StatusCode = 503;
            await context.Response.WriteAsync("Service is in maintenance mode");
            return;
        }

        await next(context);
    }
}
