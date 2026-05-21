using Microsoft.Extensions.DependencyInjection;

namespace BPOR.Rms.Api;

public static class DiExtensions
{
    public static void AddVolunteerInformationPages(this IServiceCollection services)
    {
        services.AddSingleton<IRrvTokenGenerator, RrvTokenGenerator>();
        services.AddOptions<RrvTokenOptions>().BindConfiguration("RrvToken");
    }
}