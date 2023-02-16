using System.Reflection;
using Application.Constants;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace StudyApi.DependencyRegistrations
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load(ProjectAssemblyNames.ApplicationAssemblyName));
            services.AddMediatR(Assembly.Load(ProjectAssemblyNames.ApplicationAssemblyName));
            
            
            return services;
        }
    }
}