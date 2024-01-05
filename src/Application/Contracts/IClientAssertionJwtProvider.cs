using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts;

public interface IClientAssertionJwtProvider
{
    Task<string> CreateClientAssertionJwtAsync(CancellationToken cancellationToken);
}
