using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NIHR.Infrastructure.EntityFrameworkCore;
using NIHR.Infrastructure.Configuration;
using ImportGeocodeData;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) => builder.AddNihrConfiguration(context.HostingEnvironment))
            .ConfigureServices((context, services) =>
            {
                services.GetSectionAndValidate<GeocodingSettings>(context.Configuration);

                var dbSettings = services.GetSectionAndValidate<DbSettings>(context.Configuration);
                var connectionString = dbSettings.Value.BuildConnectionString();

                services.AddDbContext<ParticipantDbContext>(options =>
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), builder =>
                    {
                        builder.UseNetTopologySuite();
                        builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    }));

                services.AddScoped<GeoCoder>();
                services.AddSingleton<IPostcodeProvider, StaticFilePostcodeMapper>();
            })
            .Build();

        var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var geoCoder = scope.ServiceProvider.GetRequiredService<GeoCoder>();

            await geoCoder.ProcessAsync();
        }
    }

}