using System.Threading.Tasks;
using Application.Models.MFA;
using Application.Responses.V1.Users;
using Dte.Common.Responses;

namespace Application.Contracts
{
    public interface IUserService
    {
        Task<Response<string>> LoginAsync(string email, string password);
        Task<Response<NhsLoginResponse>> NhsLoginAsync(string code, string redirectUrl);
        Task<Response<SignUpResponse>> NhsSignUpAsync(bool consent, string token);
        Task<Response<SignUpResponse>> SignUpAsync(string email, string password);
        Task<Response<object>> ConfirmSignUpAsync(string code, string userId);
        Task<Response<SignUpResponse>> AdminCreateUserSetPasswordAsync(string email, string password);
        Task<Response<object>> DeleteUserAsync(string accessToken);
        Task<bool> UserExistsAsync(string email);
        Task<PasswordPolicyTypeResponse> GetPasswordPolicyTypeAsync();
        Task<Response<ResendConfirmationCodeResponse>> ResendVerificationEmailAsync(string userId);
        Task<Response<ForgotPasswordResponse>> ForgotPasswordAsync(string email);

        Task<Response<ConfirmForgotPasswordResponse>> ConfirmForgotPasswordAsync(string code, string userId,
            string password);

        Task<Response<object>> ChangePasswordAsync(string participantId, string newPassword);
        Task<Response<object>> ChangeEmailAsync(string currentEmail, string newEmail);

    }
}
