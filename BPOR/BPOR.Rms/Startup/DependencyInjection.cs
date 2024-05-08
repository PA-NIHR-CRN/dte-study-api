using System.Reflection;
using BPOR.Domain.Entities;
using BPOR.Domain.Settings;
using BPOR.Infrastructure.Clients;
using BPOR.Rms.Services;
using Dte.Common.Authentication;
using Dte.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure;
using NIHR.Infrastructure.AspNetCore.DependencyInjection;
using NIHR.Infrastructure.EntityFrameworkCore;
using NIHR.Infrastructure.Interfaces;
using NIHR.Infrastructure.Configuration;
using BPOR.Registration.Stream.Handler.Services;
using Newtonsoft.Json;

namespace BPOR.Rms.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        services.AddControllersWithViews().AddRazorRuntimeCompilation();

        services.AddScoped<IEmailCampaignService, EmailCampaignService>();
        services.AddScoped<IFilterService, FilterService>();
        services.AddScoped<IPostcodeMapper, LocationApiClient>();

        services.AddScoped<ICurrentUserIdProvider<int>, SimpleCurrentUserIdProvider<int>>();
        services.AddScoped<ICurrentUserIdAccessor<int>, SimpleCurrentUserIdAccessor<int>>();

        services.AddScoped<ICurrentUserProvider<User>, CurrentUserProvider<User>>();

        services.AddTransient<IRefDataService, RefDataService>();

        // TODO: Temporary services
        services.AddTransient<IRandomiser>(p => new Randomiser(Random.Shared));
        services.AddTransient<INotificationService, NullNotificationService>();

        services.AddDistributedMemoryCache();
        services.AddPaging();

        // TODO this could be reusable
        var dbSettings = services.GetSectionAndValidate<DbSettings>(configuration);
        
        // check for dbSettings.Value is null
        if (dbSettings.Value is null)
        {
            throw new ArgumentException("DbSettings configuration is required.", nameof(dbSettings));
        }
        
        var connectionString = dbSettings.Value.BuildConnectionString();

        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), builder =>
            {
                builder.UseNetTopologySuite();
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }));

        var logger = services.BuildServiceProvider().GetService<ILoggerFactory>()
            ?.CreateLogger("BPOR.Rms");

        // TODO: Client settings are not being validated.
        var clientsSettings = services.GetSectionAndValidate<ClientsSettings>(configuration);
        var awsSettings = services.GetSectionAndValidate<AwsSettings>(configuration);
        
        // debug information
        logger?.LogCritical("AWS settings: {@AwsSettings}", JsonConvert.SerializeObject(awsSettings.Value, Formatting.Indented));
        logger?.LogCritical("Db settings: {@DbSettings}", JsonConvert.SerializeObject(dbSettings.Value, Formatting.Indented));
        logger?.LogCritical("Client settings: {@ClientsSettings}", JsonConvert.SerializeObject(clientsSettings.Value, Formatting.Indented));

        if (clientsSettings?.Value?.LocationService?.BaseUrl is null)
        {
            string clientSettingsJson = JsonConvert.SerializeObject(clientsSettings.Value, Formatting.Indented);
            throw new ArgumentException($"LocationService configuration is required. Current settings: {clientSettingsJson}", nameof(clientsSettings));
        }

        services.AddHttpClientWithRetry<IPostcodeMapper, LocationApiClient>(clientsSettings?.Value?.LocationService, 2,
            logger);

        services.AddHealthChecks().AddMySql(connectionString).AddCheck<LocationHealthCheck>("Location", Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded);

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
