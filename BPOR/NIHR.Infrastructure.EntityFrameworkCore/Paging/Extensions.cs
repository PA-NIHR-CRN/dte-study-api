using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace NIHR.Infrastructure.EntityFrameworkCore.Paging
{
    public static class Extensions
    {
        public static PageDeferred<T> DeferredPage<T>(this IOrderedQueryable<T> source, int pageSize, int currentPage)
        {
            var items = source.PageItems(pageSize, currentPage).Future();
            var totalCount = source.DeferredLongCount().FutureValue();

            return new PageDeferred<T>(items, pageSize, currentPage, totalCount);
        }

        public static Page<T> Page<T>(this IOrderedQueryable<T> source, int pageSize, int currentPage)
        {
            var items = source.PageItems(pageSize, currentPage).ToList();
            var totalCount = source.LongCount();

            return new Page<T>(items, pageSize, currentPage, totalCount);
        }

        public static async Task<Page<T>> PageAsync<T>(this IOrderedQueryable<T> source, int pageSize, int currentPage, CancellationToken token = default)
        {
            var items = await source.PageItems(pageSize, currentPage).ToListAsync(token);
            var totalCount = await source.LongCountAsync(token);

            return new Page<T>(items, pageSize, currentPage, totalCount);
        }

        private static IQueryable<T> PageItems<T>(this IOrderedQueryable<T> source, int pageSize, int currentPage) => source.Skip((currentPage - 1) * pageSize).Take(pageSize);
    }
}
