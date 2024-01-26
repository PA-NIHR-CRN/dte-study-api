using System.Threading.Tasks;
using Application.Models.MFA;
using Application.Responses.V1.Users;
using Dte.Common.Responses;

namespace Application.Contracts
{
    public interface IMfaService
    {
        Task<Response<string>> RespondToMfaChallengeAsync(string code, string mfaDetails);
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
    }
}
