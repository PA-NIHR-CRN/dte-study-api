using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NIHR.Rts.Client.Settings;

namespace NIHR.Rts.Client;

public static class DiExtensions
{
    public static IServiceCollection AddRtsServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var rtsApiSettings = services.GetSectionAndValidate<RtsApiSettings>(configuration);

        services.AddHttpClient<IRtsAddressSource, RtsAddressSource>(client =>
        {
            client.BaseAddress = new Uri(rtsApiSettings.Value.BaseUrl);
        });

        services.AddHttpClient<TokenService>(client =>
        {
            client.BaseAddress = new Uri(rtsApiSettings.Value.TokenUrl);
        });

        return services;
    }
}