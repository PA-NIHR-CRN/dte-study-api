using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Settings;
using BPOR.Rms.VolunteerInformation.Tokens;
using Microsoft.Extensions.DependencyInjection;
using NIHR.NotificationService;

namespace BPOR.Rms.VolunteerInformation;

public static class DiExtensions
{
     public static void AddVolunteerInformation(this IServiceCollection services)
    {
        services.AddScoped<IVipTokenGenerator, VipTokenGenerator>();
      
        services.AddOptions<LocalVipFileRepositoryOptions>().Configure(i => i.Path = "c:\\temp");
        services.AddScoped<IVipRepository, TempFolderVipFileRepository>();
        services.AddScoped<IStudyRepository, StudyDbRepository>();
        services.AddOptions<VipSettings>().BindConfiguration("Vip");

        services.AddNotificationDeliveryHandler<ResearcherEmailNotificationDeliveryHandler>();
    }
}