using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using BPOR.Domain.Settings;
using BPOR.Domain.Utils;
using BPOR.Infrastructure.Constants;
using BPOR.Infrastructure.Enum;
using BPOR.Infrastructure.Exceptions;
using BPOR.Infrastructure.Interfaces;
using BPOR.Infrastructure.Models.Mfa;
using Dte.Common;
using Dte.Common.Contracts;
using Dte.Common.Exceptions.Common;
using Dte.Common.Models;
using Dte.Common.Responses;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BPOR.Infrastructure.Services;

public class MfaService(
    IDataProtector dataProtector,
    IAmazonCognitoIdentityProvider provider,
    AwsSettings awsSettings,
    ILogger<MfaService> logger,
    IAuthService authService,
    IParticipantService participantService,
    IContentfulService contentfulService,
    IEmailService emailService,
    IOptions<ContentfulSettings> contentfulSettings)
    : IMfaService
{
    public async Task<Response<string>> SetUpMfaAsync(string mfaDetails, CancellationToken cancellationToken)
    {
        // set up mfa device on cognito.  This may be a phone or a software token
        var mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, mfaDetails);
        var username = mfaLoginDetails.Username;
        var password = mfaLoginDetails.Password;

        // set up the mfa device on cognito with sms
        var request = new AdminSetUserMFAPreferenceRequest
        {
            UserPoolId = awsSettings.CognitoPoolId,
            Username = username,
            SMSMfaSettings = new SMSMfaSettingsType
            {
                Enabled = true,
                PreferredMfa = true,
            },
        };

        var response = await provider.AdminSetUserMFAPreferenceAsync(request, cancellationToken);

        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            logger.LogError("AWS Cognito returned a response without an AuthenticationResult");
            return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.AuthenticationError,
                "Authentication Result from the service provider was null");
        }

        // log the user in again to get the new tokens with mfa enabled
        var loginResponse = await authService.LoginAsync(username, password, cancellationToken);

        if (!loginResponse.IsSuccess)
        {
            var newMfaDetails = loginResponse.Errors.First().Detail;
            return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.MfaSmsChallenge,
                newMfaDetails);
        }

        return Response<string>.CreateSuccessfulContentResponse(response.HttpStatusCode.ToString());
    }

    public async Task<Response<string>> VerifySoftwareTokenAsync(string code, string sessionId, string mfaDetails,
        CancellationToken cancellationToken)
    {
        var tokenRequest = new VerifySoftwareTokenRequest
        {
            Session = sessionId,
            UserCode = code,
        };

        try
        {
            var response = await provider.VerifySoftwareTokenAsync(tokenRequest, cancellationToken);

            if (response.Status != VerifySoftwareTokenResponseType.SUCCESS)
            {
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AuthenticationError, "Invalid MFA code");
            }

            var mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, mfaDetails);
            return await LoginAndHandleResponse(mfaLoginDetails.Username, mfaLoginDetails.Password,
                ErrorCode.MfaSoftwareTokenChallenge, cancellationToken);
        }
        catch (CodeMismatchException ex)
        {
            return HandleMfaException(ex, ErrorCode.MfaCodeMismatch);
        }
        catch (NotAuthorizedException ex)
        {
            if (ex.Message == "Invalid session for the user, session is expired.")
            {
                return HandleMfaException(ex, ErrorCode.MfaSessionExpired);
            }
            else
            {
                return HandleMfaException(ex, "Not_Authorized");
            }
        }
        catch (EnableSoftwareTokenMFAException ex)
        {
            if (ex.Message == "Code mismatch")
            {
                return HandleMfaException(ex, ErrorCode.MfaCodeMismatch);
            }
            else
            {
                return HandleMfaException(ex, "Error");
            }
        }
    }

    public async Task UpdateCognitoPhoneNumberAsync(string mfaDetails, string phoneNumber,
        CancellationToken cancellationToken)
    {
        try
        {
            // get the username from the mfa details
            var mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, mfaDetails);
            var username = mfaLoginDetails.Username;

            // ensure the phone number is in the correct format for cognito ie +441234567890
            phoneNumber = MfaUtils.CleanPhoneNumber(phoneNumber);

            // check and see if the user already has a phone number
            var user = await provider.AdminGetUserAsync(new AdminGetUserRequest
            {
                Username = username,
                UserPoolId = awsSettings.CognitoPoolId
            }, cancellationToken);

            var existingPhoneNumber = user.UserAttributes.FirstOrDefault(x => x.Name == "phone_number")?.Value;

            var request = new AdminUpdateUserAttributesRequest
            {
                UserPoolId = awsSettings.CognitoPoolId,
                Username = username,
                UserAttributes = [new AttributeType { Name = "phone_number", Value = phoneNumber }]
            };

            // if user has a phone number send an email to confirm the change
            if (!string.IsNullOrEmpty(existingPhoneNumber))
            {
                // get user email
                var participant = await participantService.GetParticipantAsync(username, cancellationToken);

                var contentfulEmailRequest = new EmailContentRequest
                {
                    EmailName = contentfulSettings.Value.EmailTemplates.MfaMobileNumberVerification,
                    FirstName = participant.Firstname,
                    SelectedLocale = new CultureInfo(participant.SelectedLocale ?? SelectedLocale.Default),
                };

                var contentfulEmail = await contentfulService.GetEmailContentAsync(contentfulEmailRequest);
                await emailService.SendEmailAsync(participant.Email, contentfulEmail.EmailSubject,
                    contentfulEmail.EmailBody, cancellationToken);
            }

            await provider.AdminUpdateUserAttributesAsync(request, cancellationToken);
        }
        catch (ValidationException e)
        {
            var exceptionResponse = Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.InternalServerError, e.Message);

            logger.LogError(e, "Error updating cognito phone number");

            throw new CognitoPhoneNumberUpdateException(exceptionResponse.ToString());
        }
    }

    public async Task<TotpTokenResult> GenerateTotpTokenAsync(string mfaDetails, CancellationToken cancellationToken)
    {
        var mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, mfaDetails);
        var username = mfaLoginDetails.Username;

        // get user and delete phone number
        var user = await provider.AdminGetUserAsync(new AdminGetUserRequest
        {
            Username = username,
            UserPoolId = awsSettings.CognitoPoolId
        }, cancellationToken);

        var phoneNumber = user.UserAttributes.FirstOrDefault(x => x.Name == "phone_number")?.Value;

        if (!string.IsNullOrEmpty(phoneNumber))
        {
            // delete the phone number
            await provider.AdminDeleteUserAttributesAsync(new AdminDeleteUserAttributesRequest
            {
                Username = username,
                UserPoolId = awsSettings.CognitoPoolId,
                UserAttributeNames = ["phone_number"]
            }, cancellationToken);

            // get a new session id
            var loginResponse = await authService.LoginAsync(username, mfaLoginDetails.Password, cancellationToken);

            var newMfaDetails = loginResponse.Errors.First().Detail;
            mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, mfaDetails);
        }

        var associateRequest = new AssociateSoftwareTokenRequest
        {
            Session = mfaLoginDetails.SessionId,
        };

        var associateResponse = await provider.AssociateSoftwareTokenAsync(associateRequest, cancellationToken);
        var secretCode = associateResponse.SecretCode;

        return new TotpTokenResult
        {
            SecretCode = secretCode,
            SessionId = associateResponse.Session,
            Username = username,
        };
    }

    public AdminRespondToAuthChallengeRequest CreateAuthChallengeRequest(string challengeName, string sessionId,
        string username, string code, string codeKey)
    {
        return new AdminRespondToAuthChallengeRequest
        {
            ClientId = awsSettings.CognitoAppClientIds[0],
            ChallengeName = ChallengeNameType.FindValue(challengeName),
            Session = sessionId,
            UserPoolId = awsSettings.CognitoPoolId,
            ChallengeResponses = new Dictionary<string, string>
            {
                { "USERNAME", username },
                { codeKey, code }
            }
        };
    }

    public async Task<Response<string>> RespondToTotpMfaChallengeAsync(string code, string mfaDetails,
        CancellationToken cancellationToken)
    {
        var mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, mfaDetails);
        var authChallengeRequest = CreateAuthChallengeRequest("SOFTWARE_TOKEN_MFA", mfaLoginDetails.SessionId,
            mfaLoginDetails.Username, code, "SOFTWARE_TOKEN_MFA_CODE");

        try
        {
            var authChallengeResponse =
                await provider.AdminRespondToAuthChallengeAsync(authChallengeRequest, cancellationToken);
            var idToken = authChallengeResponse.AuthenticationResult.IdToken;

            return Response<string>.CreateSuccessfulContentResponse(idToken);
        }
        catch (CodeMismatchException ex)
        {
            return HandleMfaException(ex, ErrorCode.MfaCodeMismatch);
        }
        catch (NotAuthorizedException ex)
        {
            if (ex.Message == "Invalid session for the user, session is expired.")
            {
                return HandleMfaException(ex, ErrorCode.MfaSessionExpired);
            }
            else
            {
                return HandleMfaException(ex, "Not_Authorized");
            }
        }
        catch (ExpiredCodeException ex)
        {
            return HandleMfaException(ex,
                ex.Message == "Your software token has already been used once."
                    ? "Mfa_Used_Token"
                    : ErrorCode.MfaCodeExpired);
        }
    }

    public async Task<Response<string>> ResendMfaChallengeAsync(string requestMfaDetails,
        CancellationToken cancellationToken)
    {
        var mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, requestMfaDetails);

        var request = new AdminInitiateAuthRequest
        {
            ClientId = awsSettings.CognitoAppClientIds[0],
            UserPoolId = awsSettings.CognitoPoolId,
            AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
            AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", mfaLoginDetails.Username },
                { "PASSWORD", mfaLoginDetails.Password }
            }
        };


        var response = await provider.AdminInitiateAuthAsync(request, cancellationToken);
        var protectedString = MfaLoginDetails.ToProtectedString(dataProtector, response, mfaLoginDetails.Password);

        if (response.ChallengeName == ChallengeNameType.SMS_MFA)
        {
            return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.MfaSmsChallenge,
                protectedString);
        }

        return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
            nameof(UserService), ErrorCode.MfaNoChallenge,
            protectedString);
    }


    public async Task<Response<string>> SendEmailOtpAsync(string requestMfaDetails, CancellationToken cancellationToken)
    {
        var mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, requestMfaDetails);
        var code = MfaUtils.GenerateOtpCode();

        var participant =
            await participantService.GetParticipantAsync(mfaLoginDetails.Username, cancellationToken);

        var contentfulEmailRequest = new EmailContentRequest
        {
            EmailName = contentfulSettings.Value.EmailTemplates.MfaEmailConfirmation,
            FirstName = participant.Firstname,
            Code = code,
            SelectedLocale = new CultureInfo(participant.SelectedLocale ?? SelectedLocale.Default),
        };

        var contentfulEmail = await contentfulService.GetEmailContentAsync(contentfulEmailRequest);

        await emailService.SendEmailAsync(participant.Email, contentfulEmail.EmailSubject,
            contentfulEmail.EmailBody, cancellationToken);

        await participantService.StoreMfaCodeAsync(mfaLoginDetails.Username, code, cancellationToken);

        return Response<string>.CreateSuccessfulContentResponse(participant.Email);
    }

    private Response<string> CreateErrorResponse(string errorCode, string errorMessage)
    {
        var errorResponse = Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
            nameof(UserService), errorCode, errorMessage);

        logger.LogError(errorMessage);

        return errorResponse;
    }

    public async Task<Response<string>> ValidateEmailOtpAsync(string requestMfaDetails, string code,
        CancellationToken cancellationToken)
    {
        var mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, requestMfaDetails);
        var mfaValidationResult =
            await participantService.ValidateMfaCodeAsync(mfaLoginDetails.Username, code, cancellationToken);

        return mfaValidationResult switch
        {
            MfaValidationResultEnum.UserNotFound =>
                CreateErrorResponse(ErrorCode.MfaUserNotFound, "User not found"),
            MfaValidationResultEnum.CodeExpired =>
                CreateErrorResponse(ErrorCode.MfaCodeExpired, "Code has expired"),
            MfaValidationResultEnum.CodeInvalid =>
                CreateErrorResponse(ErrorCode.MfaCodeMismatch, "Code is invalid"),
            MfaValidationResultEnum.Success => Response<string>.CreateSuccessfulResponse(
            ),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public async Task<Response<string>> ReissueMfaSessionAsync(string requestMfaDetails,
        CancellationToken cancellationToken)
    {
        var mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, requestMfaDetails);
        return await LoginAndHandleResponse(mfaLoginDetails.Username, mfaLoginDetails.Password,
            ErrorCode.MfaReissueSession, cancellationToken);
    }

    public async Task<string> GetMaskedMobile(string requestMfaDetails)
    {
        var mfaLoginDetails = MfaLoginDetails.FromProtectedString(dataProtector, requestMfaDetails);

        var phoneNumber = mfaLoginDetails.PhoneNumber;

        return string.IsNullOrEmpty(phoneNumber) ? string.Empty : phoneNumber;
    }

    public Response<string> HandleMfaException(Exception ex, string errorType)
    {
        var exceptionResponse = Response<string>.CreateErrorMessageResponse(
            ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), errorType,
            ex.Message);

        logger.LogError(ex, "{ErrorType} occurred\\\\r\\\\n{{SerializeObject}}",
            JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented), errorType);

        return exceptionResponse;
    }

    private async Task<Response<string>> LoginAndHandleResponse(string username, string password,
        string errorDetail, CancellationToken cancellationToken)
    {
        var loginResponse = await authService.LoginAsync(username, password, cancellationToken);

        if (loginResponse.IsSuccess)
            return Response<string>.CreateSuccessfulContentResponse(loginResponse.Content);

        var newMfaDetails = loginResponse.Errors.First().Detail;
        return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
            errorDetail, newMfaDetails);
    }
}
