using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NIHR.Infrastructure.AspNetCore.Services;
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

        public static IServiceCollection AddRedirectValidation(this IServiceCollection services)
        {
            
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRedirectValidator, RedirectValidator>();
            services.AddOptions<RedirectValidationSettings>().BindConfiguration("RedirectValidation");
            return services;
        }

        public static MvcOptions AddRedirectUrlValidation(this MvcOptions options)
        {
            options.Filters.Add<RedirectUrlExceptionFilter>();
            options.ModelBinderProviders.Insert(0, new RedirectUrlModelBinderProvider());
            return options;
        }
    }
}
