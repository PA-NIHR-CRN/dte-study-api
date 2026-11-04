using BPOR.Rms.Infrastructure.Paging;
using BPOR.Rms.Infrastructure.Paging.Extensions;

namespace NIHR.Infrastructure.Paging
{
    public static class AspNetCorePagingExtensions
    {
        public static Page<T> Page<T>(this IOrderedEnumerable<T> source, IPaginationService paginationService) => source.Page(paginationService.PageSize, paginationService.Page);
    }
}
