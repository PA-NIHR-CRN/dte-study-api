using Amazon.S3;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Settings;
using BPOR.Rms.VolunteerInformation.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;
using NIHR.NotificationService;

namespace BPOR.Rms.VolunteerInformation;

public static class DiExtensions
{
     public static void AddVolunteerInformation(this IServiceCollection services)
    {
        services.AddScoped<IVipTokenGenerator, VipTokenGenerator>();
      
        services.AddScoped<IVipRepository, S3VipRepository>();
        services.AddScoped<IStudyRepository, DbStudyRepository>();
        services.AddScoped<ICampaignParticipantRepository, DbCampaignParticipantRepository>();
        services.AddScoped<VipSyncInterceptor>();
        
        services.AddOptions<VipSettings>().BindConfiguration("Vip");

        services.AddNotificationDeliveryHandler<ResearcherEmailNotificationDeliveryHandler>();

        services.AddApiKeyRoleFromOptions<VipSettings>(i => i.BporContentApiKey, Roles.RoleBporContent);
        services.AddApiKeyRoleFromOptions<VipSettings>(i => i.RrvPrescreenerApiKey, Roles.RoleRrvPrescreener);

        services.AddDefaultAWSOptions(i => i.GetRequiredService<IConfiguration>().GetAWSOptions());
        services.AddAWSService<IAmazonS3>();
    }

    public static DbContextOptionsBuilder AddVipSynchronisation(this DbContextOptionsBuilder dbContextOptionsBuilder,
        IServiceProvider serviceProvider)
    {
        dbContextOptionsBuilder.AddInterceptors(serviceProvider.GetRequiredService<VipSyncInterceptor>());
        return dbContextOptionsBuilder;
    }
}