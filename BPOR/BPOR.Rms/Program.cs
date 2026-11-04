using BPOR.Domain.Entities.Configuration;
using BPOR.Infrastructure.Services.Development;
using BPOR.Rms;
using BPOR.Rms.Ms4;
using BPOR.Rms.Ms4.ScheduledJobs;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using BPOR.Rms.Infrastructure.AspNetCore.Authentication.AccessToken;
using BPOR.Rms.Infrastructure.AspNetCore.Authorization;
using BPOR.Rms.Infrastructure.Configuration.Extensions;
using BPOR.Rms.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Quartz;

var builder = WebApplication
    .CreateBuilder(args);

builder.AddNihrConfiguration();

builder.AddIdgAuthentication(authOptions =>
    {
        authOptions.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireRole(RoleConfiguration.GetRoles().Select(x => x.Code))
            .Build();
        authOptions.AddPolicy(PolicyNames.IsAdmin, policy =>
        {
            policy.Requirements.Add(new RolesAuthorizationRequirement(["Admin"]));
        });
        authOptions.AddPolicy(PolicyNames.IsResearcher, policy =>
        {
            policy.Requirements.Add(new RolesAuthorizationRequirement(["Researcher"]));
        });
    }
);

builder.Services.AddQuartz();
builder.Services.AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

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

await app.ScheduleDraftStudyCleanup();

app.Run();
