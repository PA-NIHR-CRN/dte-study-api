﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace NIHR.Infrastructure.AspNetCore.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddPaging(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services.AddScoped<IPaginationService, PaginationService>();
        }
    }
}
