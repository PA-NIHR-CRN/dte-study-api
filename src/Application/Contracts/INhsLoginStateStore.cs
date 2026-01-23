using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts;

public interface INhsLoginStateStore
{
    Task StoreAsync(string state, string nonce, CancellationToken ct);
    Task<string?> GetAndDeleteAsync(string state, CancellationToken ct);
}
