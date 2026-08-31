using BPOR.Domain.Entities.Configuration;
using BPOR.Infrastructure.Services.Development;
using BPOR.Rms.Jobs;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Interfaces;
using NIHR.Quartz;
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
    }
);

builder.Services.AddNihrQuartz(async (services, scheduler) =>
    {
        var job = JobBuilder.Create<RemoveStaleDraftStudiesJob>()
            .Build();
        var trigger = TriggerBuilder.Create()
            .WithCronSchedule(services.GetRequiredService<IOptions<DraftStudiesSettings>>().Value.StaleDraftRemovalSchedule, cs => cs
                .InTimeZone(TimeZoneInfo.Local)
                .WithMisfireHandlingInstructionFireAndProceed())
            .Build();
        await scheduler.ScheduleJob(job, trigger);
    }
    );

builder.AddAWSSystemsManagerDataProtection("/BPOR/RMS");

builder.Services.RegisterServices(builder.Configuration, builder.Environment);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOptions<BPOR.Domain.Settings.DevelopmentSettings>().BindConfiguration("DevelopmentSettings");
    builder.Services.Decorate<IEmailService, DevelopmentEmailService>();
}

builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

app.ConfigureSwagger(builder.Environment);

app.UseApplicationMiddleware();

app.Run();
