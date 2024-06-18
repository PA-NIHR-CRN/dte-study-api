using System.Security.Cryptography;

namespace BPOR.Infrastructure.Interfaces;

public interface IPrivateKeyProvider
{
    Task<RSA> GetPrivateKeyAsync(CancellationToken cancellationToken);
}
