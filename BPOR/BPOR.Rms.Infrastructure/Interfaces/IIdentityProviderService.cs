using System.Threading;
using System.Threading.Tasks;

namespace BPOR.Rms.Infrastructure.Interfaces
{
    public interface IIdentityProviderService
    {
        Task<string> GetOrAcquireTokenAsync(CancellationToken cancellationToken = default);
    }
}
