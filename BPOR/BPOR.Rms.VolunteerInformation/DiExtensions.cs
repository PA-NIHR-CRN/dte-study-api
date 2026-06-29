using Amazon.S3;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Settings;
using BPOR.Rms.VolunteerInformation.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NIHR.NotificationService;

namespace BPOR.Rms.VolunteerInformation;

public static class DiExtensions
{
     public static void AddVolunteerInformation(this IServiceCollection services)
    {
        services.AddScoped<IVipTokenGenerator, VipTokenGenerator>();
      
        services.AddScoped<IVipRepository, S3VipRepository>();
        services.AddScoped<IStudyRepository, StudyDbRepository>();
        services.AddScoped<ICampaignParticipantRepository, CampaignParticipantRepository>();

        services.AddOptions<VipSettings>().BindConfiguration("Vip");

        services.AddNotificationDeliveryHandler<ResearcherEmailNotificationDeliveryHandler>();

        services.AddDefaultAWSOptions(i => i.GetRequiredService<IConfiguration>().GetAWSOptions());
        services.AddAWSService<IAmazonS3>();
    }
}