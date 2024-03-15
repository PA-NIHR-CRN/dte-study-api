using AspNetCoreRateLimit;
using BPOR.Registration.Api.Middleware;
using Microsoft.AspNetCore.CookiePolicy;
using Newtonsoft.Json;

namespace BPOR.Registration.Api.Startup;

public static class ConfigureMiddleware
{
    public static WebApplication UseApplicationMiddleware(this WebApplication app, IHostEnvironment hostEnvironment)
    {
        app.UseIpRateLimiting();
        app.UseHttpsRedirection();
        app.UseMiddleware<CustomExceptionMiddleware>();
        // app.UseCustomHeaderForwarderHandler();

        app.UseRouting();

        if (hostEnvironment.IsDevelopment())
        {
            app.UseCors("AllowLocal");
        }

        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = hostEnvironment.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict,
            Secure = hostEnvironment.IsDevelopment()
                ? CookieSecurePolicy.SameAsRequest
                : CookieSecurePolicy.Always,
            OnAppendCookie = context =>
            {
                if (context.CookieName == ".BPOR.Session")
                {
                    SetSessionExpiryCookie(context);
                }
            }
        });

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<SessionMiddleware>();

        app.UseWhen(context =>
                // bypass maintainence mode middleware for configuration controller
                !context.Request.Path.StartsWithSegments("/api/configuration"),
            config => config.UseMiddleware<MaintenanceMiddleware>()
        );

        app
            .UseHsts()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Study API V0.1.2"); });
            });

        return app;
    }

    private static void SetSessionExpiryCookie(AppendCookieContext context)
    {
        var issuedAt = DateTimeOffset.UtcNow;
        var expiresAt = issuedAt.Add(TimeSpan.FromMinutes(10));

        var content = JsonConvert.SerializeObject(
            new { issuedAt, expiresAt },
            new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat }
        );

        if (string.IsNullOrWhiteSpace(context.CookieValue))
        {
            context.Context.Response.Cookies.Delete(context.CookieName + ".Expiry");
        }
        else
        {
            context.Context.Response.Cookies.Append(
                context.CookieName + ".Expiry",
                content,
                new CookieOptions
                {
                    HttpOnly =
                        false, // Cookie must be accessible from the client to allow session expiry notification
                    Secure = context.CookieOptions.Secure,
                    SameSite = context.CookieOptions.SameSite,
                    IsEssential = true
                });
        }
    }
}
