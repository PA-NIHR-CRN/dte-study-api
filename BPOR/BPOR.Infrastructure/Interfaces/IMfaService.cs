using BPOR.Infrastructure.Models.Mfa;
using Dte.Common.Responses;

namespace BPOR.Infrastructure.Interfaces;

public interface IMfaService
{
    Task<Response<string>> SetUpMfaAsync(string mfaDetails, CancellationToken cancellationToken);
    Task<Response<string>> VerifySoftwareTokenAsync(string code, string sessionId, string mfaDetails,
        CancellationToken cancellationToken);
    Task UpdateCognitoPhoneNumberAsync(string mfaDetails, string phoneNumber, CancellationToken cancellationToken);
    Task<TotpTokenResult> GenerateTotpTokenAsync(string mfaDetails, CancellationToken cancellationToken);
    Task<Response<string>> RespondToTotpMfaChallengeAsync(string code, string mfaDetails,
        CancellationToken cancellationToken);
    Task<Response<string>> ResendMfaChallengeAsync(string requestMfaDetails, CancellationToken cancellationToken);
    Task<Response<string>> SendEmailOtpAsync(string requestMfaDetails, CancellationToken cancellationToken);
    Task<Response<string>> ValidateEmailOtpAsync(string requestMfaDetails, string code,
        CancellationToken cancellationToken);
    Task<Response<string>> ReissueMfaSessionAsync(string requestMfaDetails, CancellationToken cancellationToken);
    Task<string> GetMaskedMobile(string requestMfaDetails);
}
