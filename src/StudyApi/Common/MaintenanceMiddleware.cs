using System.Threading.Tasks;
using Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace StudyApi.Common;

public class MaintenanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptionsMonitor<DevSettings> _devSettings;
    public MaintenanceMiddleware(IOptionsMonitor<DevSettings> devSettings, RequestDelegate next)
    {
        _devSettings = devSettings;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (_devSettings.CurrentValue.IsInMaintenance)
        {
            context.Response.StatusCode = 503;
            await context.Response.WriteAsync("Service is in maintenance mode");
            return;
        }

        await _next(context);
    }
}
