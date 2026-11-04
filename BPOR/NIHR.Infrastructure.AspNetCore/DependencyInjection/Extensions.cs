using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NIHR.Infrastructure.AspNetCore.Filters;
using NIHR.Infrastructure.AspNetCore.Services;
using NIHR.Infrastructure.AspNetCore.Settings;
using NIHR.Infrastructure.Paging;
using NIHR.Infrastructure.Settings;

namespace NIHR.Infrastructure.AspNetCore.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddPaging(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services.AddScoped<IPaginationService, RequestQueryParamPaginationService>();
        }

        public static IServiceCollection AddRedirectUriValidation(this IServiceCollection services)
        {
            
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRedirectUriValidator, RedirectUriValidator>();
            services.AddOptions<RedirectUriValidationSettings>().BindConfiguration("RedirectUriValidation");
            return services;
        }

        public static MvcOptions AddRedirectUriValidation(this MvcOptions options)
        {
            options.Filters.Add<RedirectUriExceptionFilter>();
            options.ModelBinderProviders.Insert(0, new UriModelBinderProvider());
            return options;
        }
    }
}
