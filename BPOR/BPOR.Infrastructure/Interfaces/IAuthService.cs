using Dte.Common.Responses;

namespace BPOR.Infrastructure.Interfaces;

public interface IAuthService
{
    Task<Response<string>> LoginAsync(string email, string password, CancellationToken cancellationToken);
    Task CreateSessionAndLoginAsync(string jwtToken, string sessionId, CancellationToken cancellationToken);
}
