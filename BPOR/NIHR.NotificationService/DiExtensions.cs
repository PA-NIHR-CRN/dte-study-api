using Microsoft.Extensions.DependencyInjection;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Services;

namespace NIHR.NotificationService;

public static class DiExtensions
{
    public static void AddNotificationService(this IServiceCollection services)
    {
        services.AddHostedService<HostedNotificationQueueService>();
        services.AddTransient<INotificationService, Services.NotificationService>();
        services.AddTransient<INotificationQueueService, Services.NotificationService>();
    }
    
    public static void AddNotificationDeliveryHandler<THandler>(this IServiceCollection services)
        where THandler : class, INotificationDeliveryHandler<THandler>
    {
        services.AddKeyedScoped<INotificationDeliveryHandler, THandler>(THandler.Key);
        services.AddScoped<INotificationService<THandler>, NotificationServiceForHandler<THandler>>();
    }
}