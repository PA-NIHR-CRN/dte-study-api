using System.Threading.Tasks;
using Application.Responses.V1.Users;
using Dte.Common.Responses;

namespace Application.Contracts;

public interface IAuthenticationService
{
    Task<Response<SignUpResponse>> SignUpAsync(string email, string password, string selectedLocale);
    Task<Response<string>> RespondToMfaChallengeAsync(string mfaCode, string mfaDetails);
}
