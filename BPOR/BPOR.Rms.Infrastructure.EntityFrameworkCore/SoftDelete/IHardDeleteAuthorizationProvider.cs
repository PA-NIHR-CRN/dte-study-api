namespace BPOR.Rms.Infrastructure.EntityFrameworkCore
{
    public interface IHardDeleteAuthorizationProvider
    {
        bool CanHardDelete();
    }
}