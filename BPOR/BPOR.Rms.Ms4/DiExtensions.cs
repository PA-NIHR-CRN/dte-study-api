using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Repositories;
using BPOR.Rms.Ms4.ScheduledJobs;
using BPOR.Rms.Ms4.Settings;
using BPOR.Rms.Ms4.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Quartz;

namespace BPOR.Rms.Ms4;

public static class DiExtensions
{
    public static void AddStudyRequest(this IServiceCollection services)
    {
        services.AddOptions<StudyCreationSettings>().BindConfiguration("StudyCreation");

        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped(typeof(IMvcFlowHelper<>), typeof(MvcFlowHelper<>));
        services.AddScoped(typeof(IMvcFlowHelper<,>), typeof(MvcFlowHelper<,>));
        services.AddMvcFlow<StudyRequestEditFlow, StudyRequestViewModel, StudyRequestEditContext>();
        services.AddScoped<IStudyDraftRepository, StudyDraftRepository>();
        services.AddValidatorsFromAssemblyContaining<StudyRequestStartViewModelValidator>();    
    }
    
    public static async Task ScheduleDraftStudyCleanup(this IHost host)
    {
        var schedulerFactory = host.Services.GetRequiredService<ISchedulerFactory>();
        var scheduler = await schedulerFactory.GetScheduler();
        var job = JobBuilder.Create<RemoveStaleDraftStudiesJob>()
            .Build();
        var settings = host.Services.GetRequiredService<IOptions<StudyCreationSettings>>();
        var trigger = TriggerBuilder.Create()
            .WithCronSchedule(settings.Value.StaleDraftRemovalSchedule, cs => cs
                .InTimeZone(TimeZoneInfo.Local)
                .WithMisfireHandlingInstructionFireAndProceed())
            .Build();
        await scheduler.ScheduleJob(job, trigger);
    }
}