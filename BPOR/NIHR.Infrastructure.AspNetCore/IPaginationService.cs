namespace BPOR.Rms.Startup
{
    public interface IPaginationService
    {
        int Page { get; }
        int PageSize { get; }
    }
}
