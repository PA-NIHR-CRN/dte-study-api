using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;
using Application.Models.MFA;
using Application.Responses.V1.Users;
using Dte.Common.Responses;
using AdminGetUserResponse = Application.Responses.V1.Users.AdminGetUserResponse;
using ConfirmForgotPasswordResponse = Application.Responses.V1.Users.ConfirmForgotPasswordResponse;
using ForgotPasswordResponse = Application.Responses.V1.Users.ForgotPasswordResponse;
using ResendConfirmationCodeResponse = Application.Responses.V1.Users.ResendConfirmationCodeResponse;
using SignUpResponse = Application.Responses.V1.Users.SignUpResponse;

namespace Application.Contracts
{
    public interface IUserService
    {
        Task<Response<string>> LoginAsync(string email, string password);
        Task<Response<NhsLoginResponse>> NhsLoginAsync(string code, string redirectUrl, string selectedLocale);
        Task<Response<SignUpResponse>> NhsSignUpAsync(bool consent, string selectedLocale, string token);
        Task<Response<object>> ConfirmSignUpAsync(string code, string userId);
        Task<Response<SignUpResponse>> AdminCreateUserSetPasswordAsync(string email, string password);
        Task<AdminGetUserResponse> AdminGetUserAsync(string email);
        Task<Response<object>> DeleteUserAsync(string accessToken);
        Task<bool> UserExistsAsync(string email);
        Task<PasswordPolicyTypeResponse> GetPasswordPolicyTypeAsync();
        Task<Response<ResendConfirmationCodeResponse>> ResendVerificationEmailAsync(string userId);
        Task<Response<ForgotPasswordResponse>> ForgotPasswordAsync(string email);

        Task<Response<ConfirmForgotPasswordResponse>> ConfirmForgotPasswordAsync(string code, string userId,
            string password);

        Task<Response<object>> ChangePasswordAsync(string participantId, string newPassword);
        Task<Response<object>> ChangeEmailAsync(string currentEmail, string newEmail);
        Task<Response<string>> SetUpMfaAsync(string mfaDetails);
        Task<Response<string>> VerifySoftwareTokenAsync(string code, string sessionId, string mfaDetails);
        Task UpdateCognitoPhoneNumberAsync(string mfaDetails, string phoneNumber);
        Task<TotpTokenResult> GenerateTotpToken(string mfaDetails);
        Task<Response<string>> RespondToTotpMfaChallengeAsync(string code, string mfaDetails);
        Task<Response<string>> ResendMfaChallenge(string requestMfaDetails);
        Task<Response<string>> SendEmailOtpAsync(string requestMfaDetails);
        Task<Response<string>> ValidateEmailOtpAsync(string requestMfaDetails, string code);
        Task<string> GetMaskedMobile(string requestMfaDetails);
        Task<Response<string>> ReissueMfaSessionAsync(string requestMfaDetails);
        Response<string> HandleMfaException(Exception ex, string errorType);
        AdminRespondToAuthChallengeRequest CreateAuthChallengeRequest(string challengeName, string sessionId,
            string username, string code, string codeKey);
        MfaLoginDetails DeserializeMfaLoginDetails(string mfaDetails);
        Task<List<string>> ValidatePassword(string password);
    }
}
