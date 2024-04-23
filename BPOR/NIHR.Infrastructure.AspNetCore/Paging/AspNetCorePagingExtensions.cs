using NIHR.Infrastructure.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHR.Infrastructure.Paging
{
    public static class AspNetCorePagingExtensions
    {
        public static Page<T> Page<T>(this IOrderedEnumerable<T> source, IPaginationService paginationService) => source.Page(paginationService.PageSize, paginationService.Page);
    }
}
