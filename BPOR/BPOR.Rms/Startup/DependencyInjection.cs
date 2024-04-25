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
using NIHR.Infrastructure.Interfaces;
using NIHR.Infrastructure.Services;
using NIHR.Infrastructure.Settings;
using NIHR.Infrastructure.Configuration;

namespace BPOR.Rms.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        services.AddControllersWithViews().AddRazorRuntimeCompilation();

        var identityProviderSettings = services.GetSectionAndValidate<IdentityProviderApiSettings>(configuration);

        services.AddScoped<IEmailCampaignService, EmailCampaignService>();
        services.AddScoped<IFilterService, FilterService>();
        services.AddScoped<IPostcodeMapper, LocationApiClient>();
        services.AddTransient<IIdentityProviderService, Wso2IdentityServerService>();
        services.AddHttpClient<IIdentityProviderService, Wso2IdentityServerService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(identityProviderSettings.Value.BaseUrl);
        });

        services.AddDistributedMemoryCache();
        services.AddPaging();

        // TODO this could be reusable
        var dbSettings = services.GetSectionAndValidate<DbSettings>(configuration);
        var connectionString = dbSettings.Value.BuildConnectionString();

        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), builder =>
            {
                builder.UseNetTopologySuite();
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }).UseNihrExtensions());

        var clientsSettings = services.GetSectionAndValidate<ClientsSettings>(configuration);

        var logger = services.BuildServiceProvider().GetService<ILoggerFactory>()
            ?.CreateLogger("BPOR.Rms");

        services.AddHttpClientWithRetry<IPostcodeMapper, LocationApiClient>(clientsSettings.Value.LocationService, 2,
            logger);
        services.AddHealthChecks();

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
