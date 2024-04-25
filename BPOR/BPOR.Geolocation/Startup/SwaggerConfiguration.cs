namespace BPOR.Geolocation.Startup;

public static class SwaggerConfiguration
{
    public static WebApplication ConfigureSwagger(this WebApplication app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BPOR API v1"));
        }
        return app;
    }
}
