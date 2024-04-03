using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Domain.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedResult<TResult>> ToPaginatedListAsync<TSource, TResult>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, TResult>> selector,
        int pageIndex,
        int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Select(selector)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PaginatedResult<TResult>(items, count, pageIndex, pageSize);
    }
    
    public class PaginatedResult<T>(IEnumerable<T> items, int count, int pageIndex, int pageSize)
    {
        public IEnumerable<T> Items { get; private set; } = items;
        public int TotalCount { get; private set; } = count;
        public int PageIndex { get; private set; } = pageIndex;
        public int TotalPages { get; private set; } = (int)Math.Ceiling(count / (double)pageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
