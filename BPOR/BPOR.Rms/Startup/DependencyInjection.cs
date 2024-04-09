using System.Reflection;
using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.Extensions;
using NIHR.Infrastructure.Interfaces;
using NIHR.Infrastructure.Services;
using NIHR.Infrastructure.Settings;

namespace BPOR.Rms.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        services.AddControllersWithViews().AddRazorRuntimeCompilation();

        var identityProviderSettings = services.GetSectionAndValidate<IdentityProviderApiSettings>(configuration);

        services.AddTransient<IIdentityProviderService, Wso2IdentityServerService>();
        services.AddHttpClient<IIdentityProviderService, Wso2IdentityServerService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(identityProviderSettings.Value.BaseUrl);
        });

        services.AddDistributedMemoryCache();

        // TODO this could be reusable
        var dbSettings = services.GetSectionAndValidate<DbSettings>(configuration);
        var connectionString = dbSettings.Value.BuildConnectionString();
        services.AddDbContext<AuroraDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

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
