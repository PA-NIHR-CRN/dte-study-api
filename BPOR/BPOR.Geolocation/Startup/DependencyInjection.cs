using System.Reflection;
using BPOR.Domain.Entities;
using Dte.Common.Authentication;
using Dte.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NIHR.Infrastructure.Clients;
using NIHR.Infrastructure.EntityFrameworkCore;
using NIHR.Infrastructure.Extensions;

namespace BPOR.Geolocation.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        services.AddDistributedMemoryCache();

        // TODO this could be reusable
        var dbSettings = services.GetSectionAndValidate<DbSettings>(configuration);
        var connectionString = dbSettings.Value.BuildConnectionString();
        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), builder =>
            {
                builder.UseNetTopologySuite();
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }));


        var clientsSettings = services.GetSectionAndValidate<ClientsSettings>(configuration);

        var logger = services.BuildServiceProvider().GetService<ILoggerFactory>()
            .CreateLogger("BPOR.Geolocation");

        services.AddHttpClientWithRetry<ILocationApiClient, LocationApiClient>(clientsSettings.Value.LocationService, 2,
            logger);


        if (hostEnvironment.IsDevelopment())
        {
            services.RegisterDevelopmentServices(configuration);
        }

        return services;
    }

    private static IServiceCollection RegisterDevelopmentServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Add Swagger generation with configuration
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BPOR API", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}
