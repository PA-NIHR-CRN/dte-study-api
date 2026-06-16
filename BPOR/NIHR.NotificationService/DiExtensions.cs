using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;
using NIHR.NotificationService.Controllers;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Services;
using NIHR.NotificationService.Settings;
using Notify.Client;

namespace NIHR.NotificationService;

public static class DiExtensions
{
    public static void AddNotificationService(this IServiceCollection services)
    {
        services.AddHostedService<HostedNotificationQueueService>();
        services.AddTransient<IDownstreamNotificationService, GovUkNotificationService>();
        services.AddTransient<INotificationService, Services.NotificationService>();
        services.AddTransient<INotificationQueueService, Services.NotificationService>();
        
        services.AddOptions<NotificationServiceSettings>().BindConfiguration("NotificationServiceSettings"); // TODO: Validation
        services.AddApiKeyRoleFromOptions<NotificationServiceSettings>(i => i.BearerToken,
            NotifyCallbackController.RoleNameGovUkNotifyCallback);
        services.AddSingleton(s =>
        {
            var options = s.GetRequiredService<IOptions<NotificationServiceSettings>>();
            return new NotificationClient(options.Value.ApiKey);
        });
    }

    public static void AddNotificationDeliveryHandler<THandler>(this IServiceCollection services)
        where THandler : class, INotificationDeliveryHandler<THandler>
    {
        services.AddKeyedScoped<INotificationDeliveryHandler, THandler>(THandler.Key);
        services.AddScoped<INotificationService<THandler>, NotificationServiceForHandler<THandler>>();
    }
}