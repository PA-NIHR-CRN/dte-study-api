using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.EntityFrameworkCore;
using NIHR.NotificationService.Entities;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Services;
using NIHR.NotificationService.Settings;

namespace NIHR.NotificationService;

public static class DiExtensions
{
    private const string HealthCheckNameNotificationDb = "NotificationDb";

    public static void AddNotificationService(this IServiceCollection services)
    {
        services.AddHostedService<HostedNotificationQueueService>();
        services.AddNotificationsDatabase();
        services.AddTransient<INotificationService, Services.NotificationService>();
        services.AddTransient<INotificationQueueService, Services.NotificationService>();
    }
    
    public static void AddNotificationDeliveryHandler<THandler>(this IServiceCollection services)
        where THandler : class, INotificationDeliveryHandler<THandler>
    {
        services.AddKeyedScoped<INotificationDeliveryHandler, THandler>(THandler.Key);
        services.AddScoped<INotificationService<THandler>, NotificationServiceForHandler<THandler>>();
    }

    private static void AddNotificationsDatabase(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<NotificationDbSettings>().BindConfiguration(NotificationDbSettings.SectionName)
            .ValidateDataAnnotations().ValidateOnStart();
        
        serviceCollection.AddDbContext<NotificationDbContext>((serviceProvider, options) =>
        {
            var dbSettings = serviceProvider.GetRequiredService<IOptions<NotificationDbSettings>>();
            var connectionString = dbSettings.Value.BuildConnectionString();
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                builder => { builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery); });
        });
        
        serviceCollection.AddHealthChecks().AddMySql(serviceProvider => 
        {
            var dbSettings = serviceProvider.GetRequiredService<IOptions<NotificationDbSettings>>();
            var connectionString = dbSettings.Value.BuildConnectionString();
            return connectionString;
        }, name: HealthCheckNameNotificationDb);
    }
}