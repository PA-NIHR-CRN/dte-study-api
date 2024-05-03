using System.Diagnostics;

namespace BPOR.Rms.Startup;

public static class ConfigureMiddleware
{
    private const string HealthCheckPath = "/api/health";

    public static WebApplication UseApplicationMiddleware(this WebApplication app)
    {
        app.UseCookiePolicy(
            new CookiePolicyOptions
            {
                Secure = Debugger.IsAttached && !app.Environment.IsProduction() ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always,
            });

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}");

        app.MapHealthChecks(HealthCheckPath).AllowAnonymous();

        return app;
    }
}
