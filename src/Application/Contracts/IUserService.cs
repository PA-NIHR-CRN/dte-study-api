using System.Threading.Tasks;
using Application.Responses.V1.Users;
using Dte.Common.Responses;

namespace Application.Contracts
{
    public interface IUserService
    {
        Task<Response<UserLoginResponse>> LoginAsync(string email, string password);
        Task<Response<SignUpResponse>> SignUpAsync(string email, string password);
        Task<Response<object>> ConfirmSignUpAsync(string code, string userId);
        Task<Response<SignUpResponse>> AdminCreateUserSetPasswordAsync(string email, string password);
        Task<AdminGetUserResponse> AdminGetUserAsync(string email);
        Task<Response<object>> DeleteUserAsync(string accessToken);
        Task<bool> UserExistsAsync(string email);
        Task<PasswordPolicyTypeResponse> GetPasswordPolicyTypeAsync();
        Task<Response<ResendConfirmationCodeResponse>> ResendVerificationEmailAsync(string userId);
        Task<Response<ForgotPasswordResponse>> ForgotPasswordAsync(string email);
        Task<Response<ConfirmForgotPasswordResponse>> ConfirmForgotPasswordAsync(string code, string email, string password);
        Task<Response<object>> ChangePasswordAsync(string accessToken, string oldPassword, string newPassword);
        Task<Response<object>> ChangeEmailAsync(string accessToken, string newEmail);
    }
}