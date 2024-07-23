using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NIHR.Infrastructure.Settings;
using NIHR.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using NIHR.Infrastructure.Authentication.IDG;
using NIHR.Infrastructure.Authentication.IDG.SCIM;

namespace BPOR.Rms.Startup;

public static class ConfigureAuthentication
{
    //TODO can this be moved to NIHR Infrastructure?  the package did not work
    public static IHostApplicationBuilder AddIdgAuthentication(this IHostApplicationBuilder builder, Action<AuthorizationOptions>? configureAuthorization = null, Action<CookieAuthenticationOptions>? configureCookieAuthentication = null)
    {
        var hostEnvironment = builder.Environment;
        var configuration = builder.Configuration;
        var services = builder.Services;

        var authenticationSettings = services.GetSectionAndValidate<AuthenticationSettings>(configuration).Value;

        if (authenticationSettings.Bypass && !hostEnvironment.IsProduction())
        {
            builder.Services.AddTransient<IUserAccountStore, NullUserAccountStore>();
            return builder;
        }

        services.AddTransient<ClientCredentialsHandler>();
        services.AddHttpClient<ClientCredentialsHandler>(client =>
        {
            client.BaseAddress = authenticationSettings.BaseUrl;
        });

        builder.Services.AddHttpClient<IUserAccountStore, Scim2UserManagement>(client =>
        {
            if (Uri.TryCreate(authenticationSettings.BaseUrl, authenticationSettings.Scim2Path, out var baseAddress))
            {
                client.BaseAddress = baseAddress;
            }
        })
        .AddHttpMessageHandler<ClientCredentialsHandler>();


        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            if (configureAuthorization is not null)
            {
                configureAuthorization(options);
            }
        });

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                if (configureCookieAuthentication is not null)
                {
                    configureCookieAuthentication(options);
                }
            })
            .AddOpenIdConnect(options =>
            {
                options.Authority = (new Uri(authenticationSettings.BaseUrl, authenticationSettings.AuthorityPath)).ToString();
                options.ClientId = authenticationSettings.ClientId;
                options.ClientSecret = authenticationSettings.ClientSecret;
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.SaveTokens = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.CallbackPath = "/signin-oidc";

                // Ensure callback URLs are HTTPS.
                // The application may be hosted as HTTP behind
                // a HTTPS terminating load balancer.
                // Callbacks must be the externally accessible URIs
                // rather than what they appear to be internally.
                options.Events.OnRedirectToIdentityProvider = MakeHttps;
                options.Events.OnRedirectToIdentityProviderForSignOut = MakeHttps;

            });

        return builder;
    }

    public static IHostApplicationBuilder AddAWSSystemsManagerDataProtection(this IHostApplicationBuilder builder, string parameterNamePrefix)
    {
        var dataprotection = builder.Services.AddDataProtection();

        var developmentSettings = builder.GetSectionAndValidate<DevelopmentSettings>();

        if (builder.Environment.IsProduction() ||
            !(developmentSettings.Value.DisableSsmDataProtection ||
                builder.Environment.IsDevelopment()
             ))
        {
            dataprotection.PersistKeysToAWSSystemsManager(parameterNamePrefix);
        }

        return builder;
    }

    private static Task MakeHttps(RedirectContext context)
    {
        context.ProtocolMessage.RedirectUri = MakeHttps(context.ProtocolMessage.RedirectUri);

        context.ProtocolMessage.PostLogoutRedirectUri = MakeHttps(context.ProtocolMessage.PostLogoutRedirectUri);

        return Task.CompletedTask;
    }

    private static string MakeHttps(string Uri)
    {
        if (string.IsNullOrWhiteSpace(Uri))
        {
            return Uri;
        }

        var builder = new UriBuilder(Uri)
        {
            Scheme = "https"
        };

        // Remove port if not redirecting to localhost
        builder.Port = string.Equals(builder.Host, "localhost", StringComparison.InvariantCultureIgnoreCase) ? builder.Port : -1;

        return builder.ToString();
    }
}
