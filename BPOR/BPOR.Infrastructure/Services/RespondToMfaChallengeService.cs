using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using BPOR.Infrastructure.Constants;
using BPOR.Infrastructure.Interfaces;
using BPOR.Infrastructure.Models.Mfa;
using Dte.Common.Exceptions.Common;
using Dte.Common.Responses;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;

namespace BPOR.Infrastructure.Services;

public class RespondToMfaChallengeService(
    IDataProtector dataProtector,
    IMfaService mfaService,
    IAmazonCognitoIdentityProvider provider,
    ILogger<RespondToMfaChallengeService> logger)
    : IRespondToMfaChallengeService
{
    public async Task<Response<string>> RespondToMfaChallengeAsync(string mfaCode, string mfaDetails)
    {
        try
        {
            var mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, mfaDetails);

            var request = mfaService.CreateAuthChallengeRequest("SMS_MFA", mfaLoginDetails.SessionId,
                mfaLoginDetails.Username,
                mfaCode, "SMS_MFA_CODE");
            var response = await provider.AdminRespondToAuthChallengeAsync(request);

            if (response?.AuthenticationResult == null)
            {
                logger.LogError("AWS Cognito returned as response without an AuthenticationResult");
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AuthenticationError,
                    "Authentication Result from the service provider was null");
            }

            return Response<string>.CreateSuccessfulContentResponse(response.AuthenticationResult.IdToken);
        }
        catch (CodeMismatchException ex)
        {
            return mfaService.HandleMfaException(ex, ErrorCode.MfaCodeMismatch);
        }
        catch (NotAuthorizedException ex)
        {
            return mfaService.HandleMfaException(ex,
                ex.Message == "Invalid session for the user, session is expired."
                    ? ErrorCode.MfaSessionExpired
                    : "Not_Authorized");
        }
    }
}
