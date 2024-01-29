using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses.V1.Users;
using Dte.Common.Responses;

namespace Application.Contracts;

public interface IPasswordService
{
    Task<List<string>> ValidatePassword(string password);
    Task<PasswordPolicyTypeResponse> GetPasswordPolicyTypeAsync();
    Task<Response<ForgotPasswordResponse>> ForgotPasswordAsync(string email);

    Task<Response<ConfirmForgotPasswordResponse>> ConfirmForgotPasswordAsync(string code, string userId,
        string password);

    Task<Response<object>> ChangePasswordAsync(string participantId, string newPassword);

}
