using BPOR.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ImportGeocodeData;
using NIHR.Infrastructure;
using NIHR.Infrastructure.Authentication;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) => builder.AddNihrConfiguration(context.HostingEnvironment))
            .ConfigureServices((context, services) =>
            {
                services.GetSectionAndValidate<GeocodingSettings>(context.Configuration);

                services.AddRmsDatabase();

                services.AddScoped<GeoCoder>();
                services.AddSingleton<IPostcodeProvider, StaticFilePostcodeMapper>();

                services.AddTransient<ICurrentUserIdProvider<int>, SimpleCurrentUserIdProvider<int>>();
                services.AddTransient<ICurrentUserIdAccessor<int>, SystemCurrentUserIdAccessor<int>>();
            })
            .Build();

        var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var geoCoder = scope.ServiceProvider.GetRequiredService<GeoCoder>();

        await geoCoder.ProcessAsync();
    }

}