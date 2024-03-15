using Amazon.CognitoIdentityProvider.Model;
using BPOR.Infrastructure.Responses.V1.Password;
using Dte.Common.Responses;

namespace BPOR.Infrastructure.Interfaces;

public interface IPasswordService
{
    Task<PasswordPolicyTypeResponse> GetPasswordPolicyTypeAsync(CancellationToken cancellationToken);
    Task<Response<ForgotPasswordResponse>> ForgotPasswordAsync(string email, CancellationToken cancellationToken);

    Task<Response<ConfirmForgotPasswordResponse>> ConfirmForgotPasswordAsync(string code, string userId,
        string password, CancellationToken cancellationToken);

    Task<Response<object>> ChangePasswordAsync(string email, string newPassword,
        CancellationToken cancellationToken);

    Task<List<string>> ValidatePasswordAsync(string password, CancellationToken cancellationToken);
}
