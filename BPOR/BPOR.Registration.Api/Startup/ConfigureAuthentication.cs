using BPOR.Registration.Api.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BPOR.Registration.Api.Startup;

public static class ConfigureAuthentication
{
    public static IHostApplicationBuilder AddAuthentication(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        var hostEnvironment = builder.Environment;

        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = Cookies.Session;
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.Cookie.SameSite = hostEnvironment.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict;

                options.Events.OnRedirectToLogin = (context) =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };

                options.Validate();
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AnyAuthenticatedUser", builder => builder
                .RequireAuthenticatedUser()
            );
        });

        return builder;
    }
}
