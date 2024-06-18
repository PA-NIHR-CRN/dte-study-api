using Amazon.CognitoIdentityProvider.Model;
using Dte.Common.Responses;

namespace BPOR.Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<ResendConfirmationCodeResponse>> ResendVerificationEmailAsync(string userId,
        CancellationToken cancellationToken);

    Task<object?> ConfirmSignUpAsync(string code, string userId, CancellationToken cancellationToken);
    Task<Response<object>> ChangeEmailAsync(string currentEmail, string newEmail, CancellationToken cancellationToken);
    Task<AdminGetUserResponse> AdminGetUserAsync(string email, CancellationToken cancellationToken);
    Task<bool> UserExistsAsync(string email, CancellationToken cancellationToken);
}
