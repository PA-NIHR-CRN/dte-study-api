using System.Reflection;
using BPOR.Domain.Entities;
using BPOR.Infrastructure.Clients;
using BPOR.Rms.Services;
using Dte.Common.Authentication;
using Dte.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure;
using NIHR.Infrastructure.AspNetCore.DependencyInjection;
using NIHR.Infrastructure.EntityFrameworkCore;
using NIHR.Infrastructure.Configuration;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Settings;
using BPOR.Rms.Utilities;
using BPOR.Rms.Utilities.Interfaces;
using NIHR.NotificationService;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Services;
using NIHR.NotificationService.Settings;
using Notify.Client;

namespace BPOR.Rms.Startup;
public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        services.AddSingleton(TimeProvider.System);

        services.AddControllersWithViews().AddRazorRuntimeCompilation();
        services.AddScoped<IEmailCampaignService, EmailCampaignService>();
        services.AddScoped<IPostcodeMapper, LocationApiClient>();
        services.AddScoped<IRefDataService, RefDataService>();
        services.AddScoped<ICurrentUserIdProvider<int>, SimpleCurrentUserIdProvider<int>>();
        services.AddScoped<ICurrentUserIdAccessor<int>, SimpleCurrentUserIdAccessor<int>>();
        services.AddScoped<ICurrentUserProvider<User>, CurrentUserProvider<User>>();
        services.AddScoped<IReferenceGenerator, ReferenceGenerator>();

        // Temporary services
        services.AddTransient<INotificationService, NotificationService>();

        services.AddDistributedMemoryCache();
        services.AddPaging();

        services.GetSectionAndValidate<AppSettings>(configuration);

        var dbSettings = services.GetSectionAndValidate<DbSettings>(configuration);
        var connectionString = dbSettings.Value.BuildConnectionString();

        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), builder =>
            {
                builder.UseNetTopologySuite();
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }));

        var logger = services.BuildServiceProvider().GetService<ILoggerFactory>()?.CreateLogger("BPOR.Rms");

        var clientsSettings = services.GetSectionAndValidate<ClientsSettings>(configuration);
        if (clientsSettings?.Value?.LocationService?.BaseUrl is null)
        {
            throw new ArgumentException("LocationService configuration is required.", nameof(clientsSettings));
        }

        services.AddHttpClientWithRetry<IPostcodeMapper, LocationApiClient>(clientsSettings?.Value?.LocationService, 2, logger);
        services.AddHealthChecks().AddMySql(connectionString).AddCheck<LocationHealthCheck>("Location", Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded);

        // Register NotificationTaskQueue and RmsTaskQueue with distinct interfaces
        services.AddSingleton<INotificationTaskQueue, NotificationTaskQueue>(provider =>
        {
            var notificationTaskQueueLogger = provider.GetRequiredService<ILogger<NotificationTaskQueue>>();
            return new NotificationTaskQueue(100, notificationTaskQueueLogger);
        });

        services.AddSingleton<IRmsTaskQueue, RmsTaskQueue>(provider =>
        {
            var rmsTaskQueueLogger = provider.GetRequiredService<ILogger<RmsTaskQueue>>();
            return new RmsTaskQueue(100, rmsTaskQueueLogger);
        });

        services.AddHostedService<HostedNotificationQueueService>();
        services.AddHostedService<HostedEmailQueueService>();

        var govNotifySettings = services.GetSectionAndValidate<NotificationServiceSettings>(configuration);
        services.AddSingleton(new NotificationClient(govNotifySettings.Value.ApiKey));

        if (hostEnvironment.IsDevelopment())
        {
            services.RegisterDevelopmentServices(configuration);
        }

        return services;
    }

    private static IServiceCollection RegisterDevelopmentServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        return services;
    }
}
