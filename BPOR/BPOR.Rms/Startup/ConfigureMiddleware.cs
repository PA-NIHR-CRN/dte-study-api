namespace BPOR.Rms.Startup;

public static class ConfigureMiddleware
{
    private const string HealthCheckPath = "/api/health";

    public static WebApplication UseApplicationMiddleware(this WebApplication app)
    {
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
            pattern: "{controller=Study}/{action=Index}/{id?}");

        app.MapHealthChecks(HealthCheckPath).AllowAnonymous();

        return app;
    }
}
