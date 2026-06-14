using BPOR.Domain.Repositories;
using BPOR.Domain.Settings;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Settings;
using Microsoft.Extensions.DependencyInjection;
using NIHR.NotificationService;
using NIHR.NotificationService.Interfaces;

namespace BPOR.Rms.VolunteerInformation;

public static class DiExtensions
{
    public static void AddVolunteerInformation(this IServiceCollection services)
    {
        services.AddOptions<LocalVsiFileRepositoryOptions>().Configure(i => i.Path = "c:\\temp");
        services.AddScoped<IVsiRepository, TempFolderVsiFileRepository>();
        services.AddScoped<IStudyRepository, StudyDbRepository>();
        services.AddOptions<VsiSettings>().BindConfiguration("Vsi");

        services.AddNotificationDeliveryHandler<ResearcherEmailNotificationDeliveryHandler>();
    }
}