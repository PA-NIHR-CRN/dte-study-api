using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NIHR.Infrastructure.Settings;
using NIHR.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace BPOR.Rms.Startup;

public static class ConfigureAuthentication
{
    //TODO can this be moved to NIHR Infrastructure?  the package did not work
    public static IHostApplicationBuilder AddIdgAuthentication(this IHostApplicationBuilder builder)
    {
        var hostEnvironment = builder.Environment;
        var configuration = builder.Configuration;
        var services = builder.Services;

        var authenticationSettings = services.GetSectionAndValidate<AuthenticationSettings>(configuration).Value;

        if(authenticationSettings.Bypass && !hostEnvironment.IsProduction())
        {
            return builder;
        }

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options => options.ExpireTimeSpan = TimeSpan.FromMinutes(10))
            .AddOpenIdConnect(options =>
            {
                options.Authority = authenticationSettings.Authority.ToString();
                options.ClientId = authenticationSettings.ClientId;
                options.ClientSecret = authenticationSettings.ClientSecret;
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.SaveTokens = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.CallbackPath = "/signin-oidc";
            });

        return builder;
    }
}
