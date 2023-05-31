using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Contracts;

public interface ISessionService
{
    Task DeleteSessionAsync(string participantId);
    Task SetSessionAsync(string participantId, string sessionId);
    Task<bool> ValidateSessionAsync(string participantId, string sessionId);
}