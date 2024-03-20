using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NIHR.Infrastructure.Extensions;
using NIHR.Infrastructure.Settings;

namespace BPOR.Rms.Startup;

public static class ConfigureAuthentication
{
    //TODO can this be moved to NIHR Infrastructure?  the package did not work
    public static IServiceCollection AddIdgAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var authenticationSettings = services.GetSectionAndValidate<AuthenticationSettings>(configuration).Value;

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.Authority = authenticationSettings.Authority;
                options.ClientId = authenticationSettings.ClientId;
                options.ClientSecret = authenticationSettings.ClientSecret;
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.SaveTokens = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.CallbackPath = "/signin-oidc";
            });

        return services;
    }
}
