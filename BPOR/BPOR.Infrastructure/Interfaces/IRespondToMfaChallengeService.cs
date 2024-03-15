using Dte.Common.Responses;

namespace BPOR.Infrastructure.Interfaces;

public interface IRespondToMfaChallengeService
{
    Task<Response<string>> RespondToMfaChallengeAsync(string mfaCode, string mfaDetails);
}
