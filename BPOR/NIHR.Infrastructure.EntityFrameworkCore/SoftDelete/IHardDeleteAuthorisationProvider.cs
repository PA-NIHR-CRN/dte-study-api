namespace NIHR.Infrastructure.EntityFrameworkCore
{
    public interface IHardDeleteAuthorisationProvider
    {
        bool CanHardDelete();
    }
}