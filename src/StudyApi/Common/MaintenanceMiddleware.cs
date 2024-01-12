using System.Threading.Tasks;
using Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace StudyApi.Common;

public class MaintenanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly DevSettings _devSettings;
    public MaintenanceMiddleware(IOptionsSnapshot<DevSettings> devSettings, RequestDelegate next)
    {
        _devSettings = devSettings.Value;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (_devSettings.IsInMaintenance)
        {
            context.Response.StatusCode = 503;
            await context.Response.WriteAsync("Service is in maintenance mode");
            return;
        }

        await _next(context);
    }
}
