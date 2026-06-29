using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BPOR.Rms.ApiClient;

public static class DiExtensions
{
    public static IServiceCollection AddRmsApiClient(this IServiceCollection services)
    {
        services.AddOptions<RmsApiClientSettings>().BindConfiguration("RmsApiClient");
        services.AddHttpClient<IRmsApiClient, RmsApiClient>((services, client) =>
        {
            var options = services.GetRequiredService<IOptions<RmsApiClientSettings>>();
            client.BaseAddress = new Uri(options.Value.BaseUrl);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer", options.Value.AuthBearerToken);
        });
        return services;
    }
}