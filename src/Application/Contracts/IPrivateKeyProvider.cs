using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IPrivateKeyProvider
    {
        Task<RSA> GetPrivateKeyAsync(CancellationToken cancellationToken);
    }
}
