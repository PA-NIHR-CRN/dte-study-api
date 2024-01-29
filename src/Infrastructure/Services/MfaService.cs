using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Application.Constants;
using Application.Content;
using Application.Contracts;
using Application.Models.MFA;
using Application.Settings;
using Dte.Common.Exceptions;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using FluentValidation.Results;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhoneNumbers;

namespace Infrastructure.Services;

public class MfaService : IMfaService
{
    private readonly ILogger<MfaService> _logger;
    private readonly IHeaderService _headerService;
    private readonly IAmazonCognitoIdentityProvider _provider;
    private readonly IDataProtector _dataProtector;
    private readonly AwsSettings _awsSettings;
    private readonly IParticipantService _participantService;
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;

    public MfaService(ILogger<MfaService> logger, IHeaderService headerService, IAmazonCognitoIdentityProvider provider,
        IDataProtector dataProtector, AwsSettings awsSettings, IParticipantService participantService,
        IEmailService emailService, IUserService userService)
    {
        _logger = logger;
        _headerService = headerService;
        _provider = provider;
        _dataProtector = dataProtector.CreateProtector("mfa.login.details");
        _awsSettings = awsSettings;
        _participantService = participantService;
        _emailService = emailService;
        _userService = userService;
    }

    public string GenerateMfaDetails(AdminInitiateAuthResponse response, string password = null)
    {
        var sessionId = response.Session;
        var username = response.ChallengeParameters["USER_ID_FOR_SRP"];
        // conditionally add phone number if it exists
        var phoneNumber = response.ChallengeParameters.ContainsKey("CODE_DELIVERY_DESTINATION")
            ? response.ChallengeParameters["CODE_DELIVERY_DESTINATION"]
            : null;


        return _dataProtector.Protect(JsonConvert.SerializeObject(new MfaLoginDetails
        {
            SessionId = sessionId,
            Username = username,
            Password = password,
            PhoneNumber = phoneNumber
        }));
    }

    private MfaLoginDetails DeserializeMfaLoginDetails(string mfaDetails) =>
        JsonConvert.DeserializeObject<MfaLoginDetails>(_dataProtector.Unprotect(mfaDetails));

    public async Task<Response<string>> ResendMfaChallenge(string mfaRequestDetails)
    {
        // resend mfa challenge
        var mfaLoginDetails = DeserializeMfaLoginDetails(mfaRequestDetails);

        var request = new AdminInitiateAuthRequest
        {
            ClientId = _awsSettings.CognitoAppClientIds[0],
            UserPoolId = _awsSettings.CognitoPoolId,
            AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
            AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", mfaLoginDetails.Username },
                { "PASSWORD", mfaLoginDetails.Password }
            }
        };

        try
        {
            var response = await _provider.AdminInitiateAuthAsync(request);
            var mfaDetails = GenerateMfaDetails(response, mfaLoginDetails.Password);

            if (response.ChallengeName == ChallengeNameType.SMS_MFA)
            {
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.MfaSmsChallenge,
                    mfaDetails, _headerService.GetConversationId());
            }

            return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), "No_Mfa_Challenge",
                mfaDetails, _headerService.GetConversationId());
        }
        catch (Exception ex)
        {
            var exceptionResponse = Response<string>.CreateExceptionResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                _headerService.GetConversationId());

            _logger.LogError(ex,
                $"Unknown error resending mfa challenge for user with username {mfaLoginDetails.Username}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");

            return exceptionResponse;
        }
    }

    public async Task<Response<string>> ReissueMfaSessionAsync(string requestMfaDetails)
    {
        var mfaLoginDetails = DeserializeMfaLoginDetails(requestMfaDetails);
        return await LoginAndHandleResponse(mfaLoginDetails.Username, mfaLoginDetails.Password,
            ErrorCode.MfaReissueSession);
    }

    public async Task<Response<string>> VerifySoftwareTokenAsync(string code, string sessionId, string mfaDetails)
    {
        var tokenRequest = new VerifySoftwareTokenRequest
        {
            Session = sessionId,
            UserCode = code,
        };

        try
        {
            var response = await _provider.VerifySoftwareTokenAsync(tokenRequest);

            if (response.Status != VerifySoftwareTokenResponseType.SUCCESS)
            {
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AuthenticationError, "Invalid MFA code",
                    _headerService.GetConversationId());
            }

            var mfaLoginDetails = DeserializeMfaLoginDetails(mfaDetails);
            return await LoginAndHandleResponse(mfaLoginDetails.Username, mfaLoginDetails.Password,
                ErrorCode.MfaSoftwareTokenChallenge);
        }
        catch (CodeMismatchException ex)
        {
            return HandleMfaException(ex, "MFA_Code_Mismatch");
        }
        catch (NotAuthorizedException ex)
        {
            if (ex.Message == "Invalid session for the user, session is expired.")
            {
                return HandleMfaException(ex, "MFA_Session_Expired");
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
                return HandleMfaException(ex, "MFA_Code_Mismatch");
            }
            else
            {
                return HandleMfaException(ex, "Error");
            }
        }
        catch (Exception ex)
        {
            return HandleMfaException(ex, "Error");
        }
    }

    private string GenerateOtpCode()
    {
        var random = new Random();
        var code = random.Next(100000, 999999).ToString();
        return code;
    }

    public async Task<Response<string>> SendEmailOtpAsync(string requestMfaDetails)
    {
        try
        {
            var mfaLoginDetails = DeserializeMfaLoginDetails(requestMfaDetails);
            var code = GenerateOtpCode();

            var htmlBody = EmailTemplate.GetHtmlTemplate().Replace("###TITLE_REPLACE1###",
                    "Confirm your email address")
                .Replace("###TEXT_REPLACE1###",
                    $"{code} is your Be Part of Research security code.")
                .Replace("###TEXT_REPLACE2###",
                    "This code will expire after 5 minutes.")
                .Replace("###TEXT_REPLACE3###",
                    "")
                .Replace("###TEXT_REPLACE4###",
                    "")
                .Replace("###LINK_REPLACE###", "")
                .Replace("###LINK_DISPLAY_VALUE_REPLACE###", "block")
                .Replace("###TEXT_REPLACE5###",
                    "")
                .Replace("###TEXT_REPLACE6###", "");

            var participant = await _participantService.GetParticipantDetailsAsync(mfaLoginDetails.Username);

            await _emailService.SendEmailAsync(participant.Email, "Be Part of Research security code", htmlBody);

            await _participantService.StoreMfaCodeAsync(mfaLoginDetails.Username, code);

            return Response<string>.CreateSuccessfulContentResponse(participant.Email,
                _headerService.GetConversationId());
        }
        catch (Exception ex)
        {
            var exceptionResponse = Response<string>.CreateExceptionResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                _headerService.GetConversationId());

            _logger.LogError(ex, "Error sending email otp code");

            return exceptionResponse;
        }
    }

    public async Task<Response<string>> RespondToTotpMfaChallengeAsync(string code, string mfaDetails)
    {
        var mfaLoginDetails = DeserializeMfaLoginDetails(mfaDetails);
        var authChallengeRequest = CreateAuthChallengeRequest("SOFTWARE_TOKEN_MFA", mfaLoginDetails.SessionId,
            mfaLoginDetails.Username, code, "SOFTWARE_TOKEN_MFA_CODE");

        try
        {
            var authChallengeResponse = await _provider.AdminRespondToAuthChallengeAsync(authChallengeRequest);
            var idToken = authChallengeResponse.AuthenticationResult.IdToken;

            return Response<string>.CreateSuccessfulContentResponse(idToken, _headerService.GetConversationId());
        }
        catch (CodeMismatchException ex)
        {
            return HandleMfaException(ex, "MFA_Code_Mismatch");
        }
        catch (NotAuthorizedException ex)
        {
            if (ex.Message == "Invalid session for the user, session is expired.")
            {
                return HandleMfaException(ex, "MFA_Session_Expired");
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
        catch (Exception ex)
        {
            return HandleMfaException(ex, "Unknown error responding to mfa challenge");
        }
    }

    public async Task<Response<string>> ValidateEmailOtpAsync(string requestMfaDetails, string code)
    {
        try
        {
            var mfaLoginDetails = DeserializeMfaLoginDetails(requestMfaDetails);
            var mfaValidationResult =
                await _participantService.ValidateMfaCodeAsync(mfaLoginDetails.Username, code);

            return mfaValidationResult switch
            {
                MfaValidationResult.UserNotFound => CreateErrorResponse(ErrorCode.MfaUserNotFound, "User not found"),
                MfaValidationResult.CodeExpired => CreateErrorResponse(ErrorCode.MfaCodeExpired, "Code has expired"),
                MfaValidationResult.CodeInvalid => CreateErrorResponse(ErrorCode.MfaCodeMismatch, "Code is invalid"),
                MfaValidationResult.Success => Response<string>.CreateSuccessfulResponse(
                    _headerService.GetConversationId()),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        catch (Exception ex)
        {
            var exceptionResponse = Response<string>.CreateExceptionResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                _headerService.GetConversationId());

            _logger.LogError(ex, "Error validating email otp code");

            return exceptionResponse;
        }
    }

    private Response<string> CreateErrorResponse(string errorCode, string errorMessage)
    {
        var errorResponse = Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
            nameof(UserService), errorCode, errorMessage, _headerService.GetConversationId());

        _logger.LogError(errorMessage);

        return errorResponse;
    }

    public Task<string> GetMaskedMobile(string requestMfaDetails)
    {
        var mfaLoginDetails = DeserializeMfaLoginDetails(requestMfaDetails);

        var phoneNumber = mfaLoginDetails.PhoneNumber;

        return Task.FromResult(string.IsNullOrEmpty(phoneNumber) ? string.Empty : phoneNumber);
    }

    private AdminRespondToAuthChallengeRequest CreateAuthChallengeRequest(string challengeName, string sessionId,
        string username, string code, string codeKey)
    {
        return new AdminRespondToAuthChallengeRequest
        {
            ClientId = _awsSettings.CognitoAppClientIds[0],
            ChallengeName = ChallengeNameType.FindValue(challengeName),
            Session = sessionId,
            UserPoolId = _awsSettings.CognitoPoolId,
            ChallengeResponses = new Dictionary<string, string>
            {
                { "USERNAME", username },
                { codeKey, code }
            }
        };
    }

    public async Task<Response<string>> RespondToMfaChallengeAsync(string mfaCode, string mfaDetails)
    {
        try
        {
            var mfaLoginDetails = DeserializeMfaLoginDetails(mfaDetails);
            var request = CreateAuthChallengeRequest("SMS_MFA", mfaLoginDetails.SessionId, mfaLoginDetails.Username,
                mfaCode, "SMS_MFA_CODE");
            var response = await _provider.AdminRespondToAuthChallengeAsync(request);

            if (response?.AuthenticationResult == null)
            {
                _logger.LogError("AWS Cognito returned as response without an AuthenticationResult");
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AuthenticationError,
                    "Authentication Result from the service provider was null", _headerService.GetConversationId());
            }

            return Response<string>.CreateSuccessfulContentResponse(response.AuthenticationResult.IdToken,
                _headerService.GetConversationId());
        }
        catch (CodeMismatchException ex)
        {
            return HandleMfaException(ex, "MFA_Code_Mismatch");
        }
        catch (NotAuthorizedException ex)
        {
            return HandleMfaException(ex,
                ex.Message == "Invalid session for the user, session is expired."
                    ? "MFA_Session_Expired"
                    : "Not_Authorized");
        }
        catch (Exception ex)
        {
            var response = HandleMfaException(ex, "Unknown error responding to mfa challenge");
            return response;
        }
    }

    private string CleanPhoneNumber(string phoneNumber)
    {
        PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();

        // Ensure it's a valid UK number using libphonenumber
        var number = phoneUtil.Parse(phoneNumber, "GB");
        if (!phoneUtil.IsValidNumber(number))
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure("PhoneNumber", $"{number} is not a valid UK number")
            }, "PhoneNumber");
        }

        // Check if the number is specifically a mobile number
        if (phoneUtil.GetNumberType(number) != PhoneNumberType.MOBILE)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure("PhoneNumber", $"{number} is not a valid UK mobile number")
            }, "PhoneNumber");
        }

        // Ensure the phone number is in the correct format for cognito i.e., +441234567890 E164
        phoneNumber = phoneUtil.Format(number, PhoneNumberFormat.E164);

        return phoneNumber;
    }

    public async Task UpdateCognitoPhoneNumberAsync(string mfaDetails, string phoneNumber)
    {
        try
        {
            // get the username from the mfa details
            var mfaLoginDetails = DeserializeMfaLoginDetails(mfaDetails);
            var username = mfaLoginDetails.Username;

            // ensure the phone number is in the correct format for cognito ie +441234567890
            phoneNumber = CleanPhoneNumber(phoneNumber);

            // check and see if the user already has a phone number
            var user = await _provider.AdminGetUserAsync(new AdminGetUserRequest
            {
                Username = username,
                UserPoolId = _awsSettings.CognitoPoolId
            });

            var existingPhoneNumber = user.UserAttributes.FirstOrDefault(x => x.Name == "phone_number")?.Value;

            var request = new AdminUpdateUserAttributesRequest
            {
                UserPoolId = _awsSettings.CognitoPoolId,
                Username = username,
                UserAttributes = new List<AttributeType>
                {
                    new AttributeType
                    {
                        Name = "phone_number",
                        Value = phoneNumber
                    }
                }
            };

            // if user has a phone number send an email to confirm the change
            if (!string.IsNullOrEmpty(existingPhoneNumber))
            {
                // get user email
                var email = user.UserAttributes.FirstOrDefault(x => x.Name == "email")?.Value;
                var htmlBody = EmailTemplate.GetHtmlTemplate().Replace("###TITLE_REPLACE1###",
                        "Be Part of Research mobile phone number verified")
                    .Replace("###TEXT_REPLACE1###",
                        $"The new mobile phone number provided to secure your account has been verified. We will send a security code to this number each time you sign in.")
                    .Replace("###TEXT_REPLACE2###",
                        "This will not change any telephone numbers stored in the personal details section of your account. Please sign in to your account if you need to change these.")
                    .Replace("###TEXT_REPLACE3###",
                        "")
                    .Replace("###TEXT_REPLACE4###",
                        "")
                    .Replace("###LINK_REPLACE###", "")
                    .Replace("###LINK_DISPLAY_VALUE_REPLACE###", "block")
                    .Replace("###TEXT_REPLACE5###",
                        "")
                    .Replace("###TEXT_REPLACE6###", "");

                await _emailService.SendEmailAsync(email, "Be Part of Research mobile phone number verified",
                    htmlBody);
            }

            await _provider.AdminUpdateUserAttributesAsync(request);
        }
        catch (ValidationException e)
        {
            var exceptionResponse = Response<string>.CreateErrorMessageResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, e.Message,
                _headerService.GetConversationId());

            _logger.LogError(e, "Error updating cognito phone number");

            throw new CognitoPhoneNumberUpdateException(exceptionResponse.ToString());
        }
        catch (Exception e)
        {
            var exceptionResponse = Response<string>.CreateExceptionResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), "Cognito_Phone_Number_Update_Exception",
                e,
                _headerService.GetConversationId());

            _logger.LogError(e, "Error updating cognito phone number");

            throw new CognitoPhoneNumberUpdateException(exceptionResponse.ToString());
        }
    }


    public async Task<TotpTokenResult> GenerateTotpToken(string mfaDetails)
    {
        try
        {
            var mfaLoginDetails = DeserializeMfaLoginDetails(mfaDetails);
            var username = mfaLoginDetails.Username;

            // get user and delete phone number
            var user = await _provider.AdminGetUserAsync(new AdminGetUserRequest
            {
                Username = username,
                UserPoolId = _awsSettings.CognitoPoolId
            });

            var phoneNumber = user.UserAttributes.FirstOrDefault(x => x.Name == "phone_number")?.Value;

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                // delete the phone number
                await _provider.AdminDeleteUserAttributesAsync(new AdminDeleteUserAttributesRequest
                {
                    Username = username,
                    UserPoolId = _awsSettings.CognitoPoolId,
                    UserAttributeNames = new List<string> { "phone_number" }
                });

                // get a new session id
                var loginResponse = await _userService.LoginAsync(username, mfaLoginDetails.Password);

                var newMfaDetails = loginResponse.Errors.First().Detail;
                mfaLoginDetails = DeserializeMfaLoginDetails(newMfaDetails);
            }

            var associateRequest = new AssociateSoftwareTokenRequest
            {
                Session = mfaLoginDetails.SessionId,
            };

            var associateResponse = await _provider.AssociateSoftwareTokenAsync(associateRequest);
            var secretCode = associateResponse.SecretCode;

            return new TotpTokenResult
            {
                SecretCode = secretCode,
                SessionId = associateResponse.Session,
                Username = username,
            };
        }
        catch (Exception e)
        {
            var exceptionResponse = Response<string>.CreateExceptionResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, e,
                _headerService.GetConversationId());

            _logger.LogError(e, "Error generating totp token");

            throw new Exception(exceptionResponse.ToString());
        }
    }


    public async Task<Response<string>> SetUpMfaAsync(string mfaDetails)
    {
        try
        {
            // set up mfa device on cognito.  This may be a phone or a software token
            var mfaLoginDetails = DeserializeMfaLoginDetails(mfaDetails);
            var username = mfaLoginDetails.Username;
            var password = mfaLoginDetails.Password;

            // set up the mfa device on cognito with sms
            var request = new AdminSetUserMFAPreferenceRequest
            {
                UserPoolId = _awsSettings.CognitoPoolId,
                Username = username,
                SMSMfaSettings = new SMSMfaSettingsType
                {
                    Enabled = true,
                    PreferredMfa = true,
                },
            };

            var response = await _provider.AdminSetUserMFAPreferenceAsync(request);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                _logger.LogError("AWS Cognito returned a response without an AuthenticationResult");
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AuthenticationError,
                    "Authentication Result from the service provider was null", _headerService.GetConversationId());
            }

            // log the user in again to get the new tokens with mfa enabled
            var loginResponse = await _userService.LoginAsync(username, password);

            if (!loginResponse.IsSuccess)
            {
                var newMfaDetails = loginResponse.Errors.First().Detail;
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.MfaSmsChallenge,
                    newMfaDetails, _headerService.GetConversationId());
            }

            return Response<string>.CreateSuccessfulContentResponse(response.HttpStatusCode.ToString(),
                _headerService.GetConversationId());
        }
        catch (Exception ex)
        {
            var exceptionResponse = Response<string>.CreateExceptionResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                _headerService.GetConversationId());
            _logger.LogError(ex, "Unknown error setting up mfa\\r\\n{SerializeObject}",
                JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented));
            return exceptionResponse;
        }
    }


    private Response<string> HandleMfaException(Exception ex, string errorType)
    {
        var exceptionResponse = Response<string>.CreateErrorMessageResponse(
            ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), errorType,
            ex.Message, _headerService.GetConversationId());

        _logger.LogError(ex, $"{errorType} occurred\\r\\n{{SerializeObject}}",
            JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented));

        return exceptionResponse;
    }

    private async Task<Response<string>> LoginAndHandleResponse(string username, string password,
        string errorDetail)
    {
        var loginResponse = await _userService.LoginAsync(username, password);

        if (loginResponse.IsSuccess)
            return Response<string>.CreateSuccessfulContentResponse(loginResponse.Content,
                _headerService.GetConversationId());

        var newMfaDetails = loginResponse.Errors.First().Detail;
        return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
            nameof(UserService), errorDetail, newMfaDetails,
            _headerService.GetConversationId());
    }
}
