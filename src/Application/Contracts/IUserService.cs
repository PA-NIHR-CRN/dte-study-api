using System.Threading.Tasks;
using Application.Models.MFA;
using Application.Responses.V1.Users;
using Dte.Common.Responses;

namespace Application.Contracts
{
    public interface IUserService
    {
        Task<Response<string>> LoginAsync(string email, string password);
        Task<Response<SignUpResponse>> SignUpAsync(string email, string password);
        Task<Response<object>> ConfirmSignUpAsync(string code, string userId);
        Task<AdminGetUserResponse>  AdminGetUserAsync(string email);
        Task<Response<ResendConfirmationCodeResponse>> ResendVerificationEmailAsync(string userId);
        Task<Response<object>> ChangeEmailAsync(string currentEmail, string newEmail);

    }
}
