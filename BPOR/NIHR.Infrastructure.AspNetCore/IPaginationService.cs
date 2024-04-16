namespace NIHR.Infrastructure.AspNetCore
{
    public interface IPaginationService
    {
        int Page { get; }
        int PageSize { get; }
    }
}
