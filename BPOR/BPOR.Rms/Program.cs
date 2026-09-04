using BPOR.Domain.Entities.Configuration;
using BPOR.Infrastructure.Services.Development;
using BPOR.Rms;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;
using NIHR.Infrastructure.AspNetCore.Authorization;
using NIHR.Infrastructure.Interfaces;

var builder = WebApplication
    .CreateBuilder(args);

builder.AddNihrConfiguration();

builder.AddIdgAuthentication(authOptions =>
    {
        authOptions.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireRole(RoleConfiguration.GetRoles().Select(x => x.Code))
            .Build();
        authOptions.AddAccessTokenPolicy(PolicyNames.IsResearcherCreatingStudy, AccessTokenRoleNames.ResearcherCreateStudy);
        authOptions.AddPolicy(PolicyNames.IsAdmin, policy =>
        {
            policy.Requirements.Add(new RolesAuthorizationRequirement(["Admin"]));
        });
    }
);

builder.AddAWSSystemsManagerDataProtection("/BPOR/RMS");

builder.Services.RegisterServices(builder.Configuration, builder.Environment);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOptions<BPOR.Domain.Settings.DevelopmentSettings>().BindConfiguration("DevelopmentSettings");
    builder.Services.Decorate<IEmailService, DevelopmentEmailService>();
}

builder.Services.AddScoped<AnyPolicyAuthorizationFilter>();

builder.WebHost.UseStaticWebAssets();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AnyPolicyAuthorizationFilter>();
}).AddRazorRuntimeCompilation();

var app = builder.Build();

app.ConfigureSwagger(builder.Environment);

app.UseApplicationMiddleware();

app.Run();
