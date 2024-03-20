using NIHR.Infrastructure.Extensions;
using NIHR.Infrastructure.Interfaces;
using NIHR.Infrastructure.Services;
using NIHR.Infrastructure.Settings;

namespace BPOR.Rms.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();

        var identityProviderSettings = services.GetSectionAndValidate<IdentityProviderApiSettings>(configuration);

        services.AddTransient<IIdentityProviderService, Wso2IdentityServerService>();
        services.AddHttpClient<IIdentityProviderService, Wso2IdentityServerService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(identityProviderSettings.Value.BaseUrl);
        });
        
        services.AddDistributedMemoryCache();

        return services;
    }
}
