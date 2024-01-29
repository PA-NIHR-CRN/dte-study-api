using System.Threading.Tasks;
using Application.Responses.V1.Users;
using Dte.Common.Responses;

namespace Application.Contracts;

public interface INhsLoginService
{
    Task<Response<SignUpResponse>> NhsSignUpAsync(bool consentRegistration, string token);
    Task<Response<NhsLoginResponse>> NhsLoginAsync(string code, string redirectUrl);
}
