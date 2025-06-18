using System.Reflection;
using Amazon;
using Amazon.SimpleEmail;
using BPOR.Domain.Entities;
using BPOR.Infrastructure.Clients;
using BPOR.Rms.Services;
using Dte.Common.Authentication;
using Dte.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure;
using NIHR.Infrastructure.AspNetCore.DependencyInjection;
using NIHR.Infrastructure.EntityFrameworkCore;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Utilities;
using BPOR.Rms.Utilities.Interfaces;
using Ganss.Xss;
using NIHR.Infrastructure.Interfaces;
using NIHR.Infrastructure.Settings;
using NIHR.NotificationService.Context;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Services;
using NIHR.NotificationService.Settings;
using Notify.Client;
using DbSettings = NIHR.Infrastructure.EntityFrameworkCore.DbSettings;
using NIHR.Infrastructure.Services;
using Microsoft.Extensions.Http;
using NIHR.Infrastructure.Authentication.IDG;

namespace BPOR.Rms.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        services.AddSingleton(TimeProvider.System);

        services.AddControllersWithViews().AddRazorRuntimeCompilation();
        services.AddHttpContextAccessor();

        // Add global HttpClient error logging
        services.AddTransient<ErrorLoggingHttpMessageHandler>();
        services.ConfigureAll<HttpClientFactoryOptions>(options =>
        {
            options.HttpMessageHandlerBuilderActions.Add(builder =>
            {
                builder.AdditionalHandlers.Add(builder.Services
                    .GetRequiredService<ErrorLoggingHttpMessageHandler>());
            });
        });

        services.AddScoped<ICampaignService, CampaignService>();
        services.AddTransient<IPostcodeMapper, LocationApiClient>();
        services.AddScoped<IRefDataService, RefDataService>();
        services.AddScoped<ICurrentUserIdProvider<int>, SimpleCurrentUserIdProvider<int>>();
        services.AddScoped<ICurrentUserIdAccessor<int>, SimpleCurrentUserIdAccessor<int>>();
        services.AddScoped<ICurrentUserProvider<User>, CurrentUserProvider<User>>();
        services.AddTransient<IReferenceGenerator, ReferenceGenerator>();

        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<ITransactionalEmailService, TransactionalEmailService>();
        services.GetSectionAndValidate<EmailSettings>(configuration);

        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IEncryptionService, ReferenceEncryptionService>();

        services.AddHostedService<GeoSpatialBackfillService>();

        services.AddDistributedMemoryCache();
        services.AddPaging();
        services.AddDataProtection();
        services.AddTransient<HtmlSanitizer>();
        services.AddContentManagement(configuration);


        var awsSettings = services.GetSectionAndValidate<AwsSecretsManagerSettings>(configuration).Value;

        var sesConfig = new AmazonSimpleEmailServiceConfig
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(awsSettings.Region)
        };
        services.AddSingleton<IAmazonSimpleEmailService>(new AmazonSimpleEmailServiceClient(sesConfig));


        _ = services.GetSectionAndValidate<RmsSettings>(configuration);
        var dbSettings = services.GetSectionAndValidate<DbSettings>(configuration);
        var participantConnectionString = dbSettings.Value.BuildConnectionString();

        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(participantConnectionString, ServerVersion.AutoDetect(participantConnectionString),
                builder =>
                {
                    builder.UseNetTopologySuite();
                    builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }));

        var notificationConnectionString =
            dbSettings.Value.BuildConnectionString(dbSettings.Value.NotificationDatabase);

        services.AddDbContext<NotificationDbContext>(options =>
            options.UseMySql(notificationConnectionString, ServerVersion.AutoDetect(notificationConnectionString),
                builder => { builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery); }));

        var logger = services.BuildServiceProvider().GetService<ILoggerFactory>()?.CreateLogger("BPOR.Rms");

        var clientsSettings = services.GetSectionAndValidate<ClientsSettings>(configuration);
        if (clientsSettings?.Value?.LocationService?.BaseUrl is null)
        {
            throw new ArgumentException("LocationService configuration is required.", nameof(clientsSettings));
        }

        services.AddHttpClientWithRetry<IPostcodeMapper, LocationApiClient>(clientsSettings?.Value?.LocationService, 2,
            logger);
        services.AddHealthChecks().AddMySql(participantConnectionString).AddCheck<LocationHealthCheck>("Location",
            Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded);

        services.AddSingleton<IRmsTaskQueue, RmsTaskQueue>(provider =>
        {
            var rmsTaskQueueLogger = provider.GetRequiredService<ILogger<RmsTaskQueue>>();
            return new RmsTaskQueue(100, rmsTaskQueueLogger);
        });

        services.AddHostedService<HostedNotificationQueueService>();
        services.AddHostedService<HostedCampaignQueueService>();

        var govNotifySettings = services.GetSectionAndValidate<NotificationServiceSettings>(configuration);
        services.AddSingleton(new NotificationClient(govNotifySettings.Value.ApiKey));

        if (hostEnvironment.IsDevelopment())
        {
            services.RegisterDevelopmentServices(configuration);
        }

        return services;
    }

    private static IServiceCollection RegisterDevelopmentServices(this IServiceCollection services,
        IConfiguration configuration)
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
