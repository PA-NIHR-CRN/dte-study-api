namespace NIHR.Infrastructure.Paging
{
    public interface IPaginationService
    {
        int Page { get; }
        int PageSize { get; }
    }
}
