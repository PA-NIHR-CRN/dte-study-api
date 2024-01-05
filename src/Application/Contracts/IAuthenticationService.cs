using System.Threading.Tasks;

namespace Application.Contracts;

public interface IAuthenticationService
{
    Task CreateSessionAndLoginAsync(string jwtToken, string sessionId);
    Task DeleteSessionAndSignOutAsync();
}
