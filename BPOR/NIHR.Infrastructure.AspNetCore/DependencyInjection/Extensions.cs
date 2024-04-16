
using BPOR.Rms.Startup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHR.Infrastructure.AspNetCore.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddPaging(this IServiceCollection services)
        {
            return services.AddScoped<IPaginationService, PaginationService>();
        }
    }
}
