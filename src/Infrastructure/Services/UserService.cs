using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Application.Constants;
using Application.Contracts;
using Application.Models.MFA;
using Application.Participants.V1.Commands.Participants;
using Application.Responses.V1.Users;
using Application.Settings;
using Domain.Entities.Participants;
using Dte.Common;
using Dte.Common.Contracts;
using Dte.Common.Exceptions;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Helpers;
using Dte.Common.Http;
using Dte.Common.Models;
using Dte.Common.Responses;
using Infrastructure.Clients;
using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhoneNumbers;
using AdminGetUserResponse = Application.Responses.V1.Users.AdminGetUserResponse;
using ConfirmForgotPasswordResponse = Application.Responses.V1.Users.ConfirmForgotPasswordResponse;
using ForgotPasswordResponse = Application.Responses.V1.Users.ForgotPasswordResponse;
using ResendConfirmationCodeResponse = Application.Responses.V1.Users.ResendConfirmationCodeResponse;
using SignUpResponse = Application.Responses.V1.Users.SignUpResponse;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IAmazonCognitoIdentityProvider _provider;
        private readonly IHeaderService _headerService;
        private readonly AwsSettings _awsSettings;
        private readonly ILogger<UserService> _logger;
        private readonly IEmailService _emailService;
        private readonly NhsLoginHttpClient _nhsLoginHttpClient;
        private readonly IMediator _mediator;
        private readonly IParticipantService _participantService;
        private readonly IDataProtector _dataProtector;
        private readonly IContentfulService _contentfulService;
        private readonly ContentfulSettings _contentfulSettings;


        public UserService(IMediator mediator, IAmazonCognitoIdentityProvider provider, IHeaderService headerService,
            AwsSettings awsSettings, ILogger<UserService> logger,
            IEmailService emailService, IParticipantService participantService,
            NhsLoginHttpClient nhsLoginHttpClient, IDataProtectionProvider dataProtector,
            IContentfulService contentfulService, ContentfulSettings contentfulSettings)

        {
            _provider = provider;
            _headerService = headerService;
            _awsSettings = awsSettings;
            _logger = logger;
            _emailService = emailService;
            _nhsLoginHttpClient = nhsLoginHttpClient;
            _mediator = mediator;
            _participantService = participantService;
            _dataProtector = dataProtector.CreateProtector("mfa.login.details");
            _contentfulService = contentfulService;
            _contentfulSettings = contentfulSettings;
        }

        public async Task<Response<string>> LoginAsync(string email, string password)
        {
            var request = new AdminInitiateAuthRequest
            {
                UserPoolId = _awsSettings.CognitoPoolId,
                ClientId = _awsSettings.CognitoAppClientIds[0],
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
            };

            request.AuthParameters.Add("USERNAME", email);
            request.AuthParameters.Add("PASSWORD", password);
            // can't get custom scopes from this call, you can only get it from a url call like the hosted UI does.. (https://github.com/aws/aws-sdk-net-extensions-cognito/issues/42)

            try
            {
                var response = await _provider.AdminInitiateAuthAsync(request);
                var protectedString = MfaLoginDetails.ToProtectedString(_dataProtector, response, password);

                if (response.ChallengeName == ChallengeNameType.MFA_SETUP)
                {
                    return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.MfaSetupChallenge,
                        protectedString, _headerService.GetConversationId());
                }

                if (response.ChallengeName == ChallengeNameType.SMS_MFA)
                {
                    return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.MfaSmsChallenge,
                        protectedString, _headerService.GetConversationId());
                }

                if (response.ChallengeName == ChallengeNameType.SOFTWARE_TOKEN_MFA)
                {
                    return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.MfaSoftwareTokenChallenge,
                        protectedString, _headerService.GetConversationId());
                }

                if (response?.AuthenticationResult != null)
                    return Response<string>.CreateSuccessfulContentResponse(response.AuthenticationResult.IdToken,
                        _headerService.GetConversationId());

                _logger.LogError("AWS Cognito returned as response without an AuthenticationResult");
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AuthenticationError,
                    "Authentication Result from the service provider was null", _headerService.GetConversationId());
            }
            catch (UserNotFoundException)
            {
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AuthenticationNotAuthorized, "", _headerService.GetConversationId());
            }
            catch (NotAuthorizedException)
            {
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AuthenticationNotAuthorized, "", _headerService.GetConversationId());
            }
            catch (UserNotConfirmedException)
            {
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AuthenticationNotAuthorized, "", _headerService.GetConversationId());
            }
            catch (LimitExceededException)
            {
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AuthenticationNotAuthorized, "", _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<string>.CreateExceptionResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                    _headerService.GetConversationId());
                _logger.LogError(ex,
                    $"Unknown error logging in user with email {email}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                return exceptionResponse;
            }
        }

        public MfaLoginDetails DeserializeMfaLoginDetails(string mfaDetails) =>
            JsonConvert.DeserializeObject<MfaLoginDetails>(_dataProtector.Unprotect(mfaDetails));


        public AdminRespondToAuthChallengeRequest CreateAuthChallengeRequest(string challengeName, string sessionId,
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

        public Response<string> HandleMfaException(Exception ex, string errorType)
        {
            var exceptionResponse = Response<string>.CreateErrorMessageResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), errorType,
                ex.Message, _headerService.GetConversationId());

            _logger.LogError(ex, $"{errorType} occurred\\r\\n{{SerializeObject}}",
                JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented));

            return exceptionResponse;
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
            catch (Exception ex)
            {
                return HandleMfaException(ex, "Unknown error responding to mfa challenge");
            }
        }

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
                var protectedString = MfaLoginDetails.ToProtectedString(_dataProtector, response, mfaLoginDetails.Password);

                if (response.ChallengeName == ChallengeNameType.SMS_MFA)
                {
                    return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.MfaSmsChallenge,
                        protectedString, _headerService.GetConversationId());
                }

                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.MfaNoChallenge,
                    protectedString, _headerService.GetConversationId());
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

                var participant = await _participantService.GetParticipantDetailsAsync(mfaLoginDetails.Username);

                var contentfulEmailRequest = new EmailContentRequest
                {
                    EmailName = _contentfulSettings.EmailTemplates.MfaEmailConfirmation,
                    FirstName = participant.Firstname,
                    Code = code,
                    SelectedLocale = new CultureInfo(participant.SelectedLocale ?? SelectedLocale.Default),
                };

                var contentfulEmail = await _contentfulService.GetEmailContentAsync(contentfulEmailRequest);

                await _emailService.SendEmailAsync(participant.Email, contentfulEmail.EmailSubject,
                    contentfulEmail.EmailBody);

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

        private Response<string> CreateErrorResponse(string errorCode, string errorMessage)
        {
            var errorResponse = Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), errorCode, errorMessage, _headerService.GetConversationId());

            _logger.LogError(errorMessage);

            return errorResponse;
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
                    MfaValidationResult.UserNotFound =>
                        CreateErrorResponse(ErrorCode.MfaUserNotFound, "User not found"),
                    MfaValidationResult.CodeExpired =>
                        CreateErrorResponse(ErrorCode.MfaCodeExpired, "Code has expired"),
                    MfaValidationResult.CodeInvalid =>
                        CreateErrorResponse(ErrorCode.MfaCodeMismatch, "Code is invalid"),
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

        public Task<string> GetMaskedMobile(string requestMfaDetails)
        {
            var mfaLoginDetails = DeserializeMfaLoginDetails(requestMfaDetails);

            var phoneNumber = mfaLoginDetails.PhoneNumber;

            return Task.FromResult(string.IsNullOrEmpty(phoneNumber) ? string.Empty : phoneNumber);
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
                    var participant = await _participantService.GetParticipantDetailsAsync(username);

                    var contentfulEmailRequest = new EmailContentRequest
                    {
                        EmailName = _contentfulSettings.EmailTemplates.MfaMobileNumberVerification,
                        FirstName = participant.Firstname,
                        SelectedLocale = new CultureInfo(participant.SelectedLocale ?? SelectedLocale.Default),
                    };

                    var contentfulEmail = await _contentfulService.GetEmailContentAsync(contentfulEmailRequest);
                    await _emailService.SendEmailAsync(participant.Email, contentfulEmail.EmailSubject,
                        contentfulEmail.EmailBody);
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
                    var loginResponse = await LoginAsync(username, mfaLoginDetails.Password);

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
                var loginResponse = await LoginAsync(username, password);

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

        private async Task<Response<string>> LoginAndHandleResponse(string username, string password,
            string errorDetail)
        {
            var loginResponse = await LoginAsync(username, password);

            if (loginResponse.IsSuccess)
                return Response<string>.CreateSuccessfulContentResponse(loginResponse.Content,
                    _headerService.GetConversationId());

            var newMfaDetails = loginResponse.Errors.First().Detail;
            return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), errorDetail, newMfaDetails,
                _headerService.GetConversationId());
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
            catch (Exception ex)
            {
                return HandleMfaException(ex, "Error");
            }
        }

        private static bool IsUnder18(DateTime dateOfBirth) => DateTime.Now.AddYears(-18).Date < dateOfBirth.Date;

        public async Task<Response<NhsLoginResponse>> NhsLoginAsync(string code, string redirectUrl,
            string selectedLocale)
        {
            try
            {
                var tokens = await _nhsLoginHttpClient.GetTokensFromAuthorizationCode(code, redirectUrl);

                var response = new NhsLoginResponse
                {
                    IdToken = tokens.IdToken,
                    AccessToken = tokens.AccessToken,
                };

                var nhsUserInfo = await _nhsLoginHttpClient.GetUserInfoAsync(tokens.AccessToken);

                // check if nhsUserInfo.DateOfBirth is under 18 and return an error if so
                if (nhsUserInfo.DateOfBirth.HasValue && IsUnder18(nhsUserInfo.DateOfBirth.Value))
                {
                    return Response<NhsLoginResponse>.CreateErrorMessageResponse(
                        ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.UserIsUnderage,
                        "User is under 18",
                        _headerService.GetConversationId()
                    );
                }

                await _participantService.NhsLoginAsync(new ParticipantDetails
                {
                    ConsentRegistration = false,
                    DateOfBirth = nhsUserInfo.DateOfBirth,
                    Email = nhsUserInfo.Email,
                    Firstname = nhsUserInfo.FirstName,
                    Lastname = nhsUserInfo.LastName,
                    NhsId = nhsUserInfo.NhsId,
                    NhsNumber = nhsUserInfo.NhsNumber,
                    SelectedLocale = selectedLocale
                });

                return Response<NhsLoginResponse>.CreateSuccessfulContentResponse(response,
                    _headerService.GetConversationId());
            }
            catch (HttpServiceException ex)
            {
                _logger.LogInformation("NhsLoginAsync():HttpServiceException handler");

                if (ex.ResponseContent !=
                    JsonConvert.SerializeObject(new { Message = ErrorCode.UnableToMatchAccounts }))
                {
                    return HandleNhsLoginException(ex);
                }

                var errorResponse = Response<NhsLoginResponse>.CreateErrorMessageResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.UnableToMatchAccounts,
                    "Unable to match account details",
                    _headerService.GetConversationId());

                return errorResponse;
            }
            catch (Exception ex)
            {
                return HandleNhsLoginException(ex);
            }
        }

        private Response<NhsLoginResponse> HandleNhsLoginException(Exception ex)
        {
            var exceptionResponse = Response<NhsLoginResponse>.CreateExceptionResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                _headerService.GetConversationId());
            _logger.LogError(ex,
                $"Unknown error logging in with NHS login\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
            return exceptionResponse;
        }

        public async Task<Response<SignUpResponse>> NhsSignUpAsync(bool consentRegistration, string selectedLocale,
            string token)
        {
            try
            {
                var nhsUserInfo = await _nhsLoginHttpClient.GetUserInfoAsync(token);

                await _mediator.Send(new CreateParticipantDetailsCommand("", nhsUserInfo.Email,
                    nhsUserInfo.FirstName, nhsUserInfo.LastName,
                    consentRegistration, nhsUserInfo.NhsId, nhsUserInfo.DateOfBirth.Value, nhsUserInfo.NhsNumber,
                    selectedLocale));

                return Response<SignUpResponse>.CreateSuccessfulContentResponse(
                    new SignUpResponse { UserConsents = true, }, _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<SignUpResponse>.CreateExceptionResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                    _headerService.GetConversationId());
                _logger.LogError(ex,
                    $"Unknown error logging in with NHS login\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                return exceptionResponse;
            }
        }

        public async Task<Response<object>> ConfirmSignUpAsync(string code, string userId)
        {
            try
            {
                var getUserResponse = await AdminGetUserAsync(userId);

                if (getUserResponse == null || getUserResponse.Status == UserStatusType.CONFIRMED)
                {
                    _logger.LogInformation($"User {userId} not found or already confirmed");
                    // Return a generic success message.
                    return Response<object>.CreateSuccessfulResponse();
                }

                var response = await _provider.ConfirmSignUpAsync(new ConfirmSignUpRequest
                {
                    ClientId = _awsSettings.CognitoAppClientIds[0],
                    ConfirmationCode = code,
                    Username = userId
                });

                if (!IsSuccessHttpStatusCode((int)response.HttpStatusCode))
                {
                    _logger.LogError($"Confirm SignUp user returned response code: {response.HttpStatusCode}");
                }

                // Return a generic success message regardless of the outcome.
                return Response<object>.CreateSuccessfulResponse();
            }
            catch (ExpiredCodeException ex)
            {
                _logger.LogError(ex, $"Expired code error during confirmation for user {userId}");
                return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.InternalServerError,
                    "An error occurred during confirmation. Please try again.", _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unknown error confirming user signup with userId {userId}");
                return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.InternalServerError,
                    "An error occurred during confirmation. Please try again.", _headerService.GetConversationId());
            }
        }


        public async Task<Response<SignUpResponse>> AdminCreateUserSetPasswordAsync(string email, string password)
        {
            try
            {
                if (await UserExistsAsync(email))
                {
                    return Response<SignUpResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.AdminCreateUserErrorUserAlreadyExists, "User already exists",
                        _headerService.GetConversationId());
                }

                var passwordErrors = await ValidatePassword(password);

                if (passwordErrors.Any())
                {
                    return Response<SignUpResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.PasswordValidationError,
                        $"Password validation errors: {string.Join("; ", passwordErrors)}",
                        _headerService.GetConversationId());
                }

                var createResponse = await _provider.AdminCreateUserAsync(new AdminCreateUserRequest
                {
                    UserPoolId = _awsSettings.CognitoPoolId,
                    Username = email,
                    MessageAction = "SUPPRESS"
                });

                if (!IsSuccessHttpStatusCode((int)createResponse.HttpStatusCode))
                {
                    return Response<SignUpResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.AdminCreateUserError, "Create user error",
                        _headerService.GetConversationId());
                }

                var setPasswordResponse = await _provider.AdminSetUserPasswordAsync(new AdminSetUserPasswordRequest
                {
                    UserPoolId = _awsSettings.CognitoPoolId, Username = email, Password = password, Permanent = true
                });

                return IsSuccessHttpStatusCode((int)setPasswordResponse.HttpStatusCode)
                    ? Response<SignUpResponse>.CreateSuccessfulResponse(_headerService.GetConversationId())
                    : Response<SignUpResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.AdminSetUserPasswordError,
                        $"Set user password response returned code: {setPasswordResponse.HttpStatusCode}",
                        _headerService.GetConversationId());
            }
            catch (UsernameExistsException ex)
            {
                return Response<SignUpResponse>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AdminCreateUserErrorUsernameExists, ex,
                    _headerService.GetConversationId());
            }
            catch (InvalidParameterException ex)
            {
                return Response<SignUpResponse>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.AdminSetUserPasswordErrorInvalidParameter, ex,
                    _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<SignUpResponse>.CreateExceptionResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                    _headerService.GetConversationId());
                _logger.LogError(ex,
                    $"Unknown error for admin create and set user password\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                return exceptionResponse;
            }
        }

        public async Task<AdminGetUserResponse> AdminGetUserAsync(string email)
        {
            try
            {
                var response = await _provider.AdminGetUserAsync(new AdminGetUserRequest
                {
                    UserPoolId = _awsSettings.CognitoPoolId,
                    Username = email
                });

                Boolean authenticatedMobileVerified = false;

                foreach (var attribute in response.UserAttributes)
                {
                    if (attribute.Name.Equals("phone_number_verified") && attribute.Value.Equals("true"))
                    {
                        authenticatedMobileVerified = true;
                    }
                }

                return IsSuccessHttpStatusCode((int)response.HttpStatusCode)
                    ? new AdminGetUserResponse
                    {
                        Email = email, Id = response.Username, Status = response.UserStatus?.ToString(),
                        CreatedDate = response.UserCreateDate, LastModifiedDate = response.UserLastModifiedDate,
                        Enabled = response.Enabled, AuthenticatedMobileVerified = authenticatedMobileVerified
                    }
                    : null;
            }
            catch (UserNotFoundException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not get user: {email}");
                throw;
            }
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            try
            {
                var response = await _provider.AdminGetUserAsync(new AdminGetUserRequest
                    { UserPoolId = _awsSettings.CognitoPoolId, Username = email });

                return response is { HttpStatusCode: HttpStatusCode.OK };
            }
            catch (UserNotFoundException)
            {
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AdminGetUserAsync failed");
                throw;
            }
        }

        public async Task<Response<object>> DeleteUserAsync(string userName)
        {
            try
            {
                var response = await _provider.AdminDeleteUserAsync(new AdminDeleteUserRequest
                {
                    UserPoolId = _awsSettings.CognitoPoolId,
                    Username = userName
                });
                if (response == null)
                {
                    return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.DeleteParticipantAccountError, "Delete user error",
                        _headerService.GetConversationId());
                }

                return IsSuccessHttpStatusCode((int)response.HttpStatusCode)
                    ? Response<object>.CreateSuccessfulResponse()
                    : Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.DeleteParticipantAccountError,
                        $"Delete user returned response code: {response.HttpStatusCode}",
                        _headerService.GetConversationId());
            }
            catch (InvalidParameterException ex)
            {
                return Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.DeleteParticipantAccountError, ex,
                    _headerService.GetConversationId());
            }
            catch (NotAuthorizedException ex)
            {
                return Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.DeleteParticipantAccountError, ex,
                    _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.InternalServerError, ex, _headerService.GetConversationId());
                _logger.LogError(ex,
                    $"Unknown error deleting participant account\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                return exceptionResponse;
            }
        }

        public async Task<PasswordPolicyTypeResponse> GetPasswordPolicyTypeAsync()
        {
            try
            {
                var describeUserPoolResponse = await _provider.DescribeUserPoolAsync(new DescribeUserPoolRequest
                    { UserPoolId = _awsSettings.CognitoPoolId });

                if (describeUserPoolResponse?.UserPool?.Policies?.PasswordPolicy == null) return null;

                var passwordPolicy = describeUserPoolResponse.UserPool.Policies.PasswordPolicy;

                return new PasswordPolicyTypeResponse
                {
                    MinimumLength = passwordPolicy.MinimumLength,
                    RequireLowercase = passwordPolicy.RequireLowercase,
                    RequireNumbers = passwordPolicy.RequireNumbers,
                    RequireSymbols = passwordPolicy.RequireSymbols,
                    RequireUppercase = passwordPolicy.RequireUppercase,
                    AllowedPasswordSymbols = string.Join(" ", PasswordHelper.SymbolList),
                    WeakPasswords = PasswordHelper.WeakPasswords
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not get Cognito password policy");

                return null;
            }
        }

        public async Task<Response<ResendConfirmationCodeResponse>> ResendVerificationEmailAsync(string userId)
        {
            try
            {
                var getUserResponse = await AdminGetUserAsync(userId);

                if (getUserResponse.Status != UserStatusType.CONFIRMED)
                {
                    var response = await _provider.ResendConfirmationCodeAsync(new ResendConfirmationCodeRequest
                    {
                        Username = userId,
                        ClientId = _awsSettings.CognitoAppClientIds[0]
                    });

                    // Log the response for internal tracking but do not return specifics to the client
                    if (!IsSuccessHttpStatusCode((int)response.HttpStatusCode))
                    {
                        _logger.LogError(
                            $"Resend verification email response returned code: {response.HttpStatusCode} for userId {userId}");
                    }
                }

                // Always return the same generic response regardless of user state or other conditions
                return Response<ResendConfirmationCodeResponse>.CreateSuccessfulResponse(
                    _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Unknown error resending verification email for userId {userId}");

                // In case of an exception also return the generic message
                return Response<ResendConfirmationCodeResponse>.CreateSuccessfulResponse(
                    _headerService.GetConversationId());
            }
        }


        public async Task<Response<ForgotPasswordResponse>> ForgotPasswordAsync(string email)
        {
            _logger.LogInformation("email: {email}", email);

            var user = await AdminGetUserAsync(email);

            _logger.LogInformation(JsonConvert.SerializeObject(user));

            if (user == null || !user.Enabled)
            {
                var participantDetails = await _participantService.GetParticipantDetailsByEmailAsync(email);
                if (string.IsNullOrWhiteSpace(participantDetails?.NhsId))
                    return Response<ForgotPasswordResponse>.CreateSuccessfulResponse(
                        _headerService.GetConversationId());

                var request = new EmailContentRequest
                {
                    EmailName = _contentfulSettings.EmailTemplates.NhsPasswordReset,
                    SelectedLocale = new CultureInfo(participantDetails.SelectedLocale ?? SelectedLocale.Default),
                    FirstName = participantDetails.Firstname,
                };

                var contentfulEmail = await _contentfulService.GetEmailContentAsync(request);

                await _emailService.SendEmailAsync(participantDetails.Email, contentfulEmail.EmailSubject,
                    contentfulEmail.EmailBody);

                return Response<ForgotPasswordResponse>.CreateSuccessfulResponse(
                    _headerService.GetConversationId());
            }

            if (user.Status == UserStatusType.CONFIRMED)
            {
                var response = await _provider.ForgotPasswordAsync(new ForgotPasswordRequest
                {
                    ClientId = _awsSettings.CognitoAppClientIds[0],
                    Username = email
                });

                if (!IsSuccessHttpStatusCode((int)response.HttpStatusCode))
                {
                    _logger.LogWarning("ForgotPasswordAsync returned: {response}",
                        JsonConvert.SerializeObject(response));
                }
            }

            return Response<ForgotPasswordResponse>.CreateSuccessfulResponse(_headerService.GetConversationId());
        }

        public async Task<Response<ConfirmForgotPasswordResponse>> ConfirmForgotPasswordAsync(string code,
            string userId, string password)
        {
            try
            {
                var passwordErrors = await ValidatePassword(password);

                if (passwordErrors.Any())
                {
                    return Response<ConfirmForgotPasswordResponse>.CreateErrorMessageResponse(
                        ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.PasswordValidationError,
                        $"Password validation errors: {string.Join("; ", passwordErrors)}",
                        _headerService.GetConversationId());
                }

                var response = await _provider.ConfirmForgotPasswordAsync(new ConfirmForgotPasswordRequest
                {
                    ClientId = _awsSettings.CognitoAppClientIds[0],
                    ConfirmationCode = code,
                    Username = userId,
                    Password = password
                });

                return IsSuccessHttpStatusCode((int)response.HttpStatusCode)
                    ? Response<ConfirmForgotPasswordResponse>.CreateSuccessfulResponse(
                        _headerService.GetConversationId())
                    : Response<ConfirmForgotPasswordResponse>.CreateErrorMessageResponse(
                        ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.ConfirmForgotPasswordError,
                        $"Confirm Forgot password response returned code: {response.HttpStatusCode}",
                        _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<ConfirmForgotPasswordResponse>.CreateExceptionResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                    _headerService.GetConversationId());
                _logger.LogError(ex,
                    $"Unknown error confirming forgot password with userId {userId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                return exceptionResponse;
            }
        }

        public async Task<Response<object>> ChangePasswordAsync(string email, string newPassword)
        {
            try
            {
                var response = await _provider.AdminSetUserPasswordAsync(new AdminSetUserPasswordRequest
                {
                    UserPoolId = _awsSettings.CognitoPoolId,
                    Username = email,
                    Password = newPassword,
                    Permanent = true
                });

                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                {
                    return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.ChangePasswordError,
                        $"Change user password returned response code: {response.HttpStatusCode}",
                        _headerService.GetConversationId());
                }

                return Response<object>.CreateSuccessfulResponse();
            }
            catch (LimitExceededException ex)
            {
                return Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.ChangePasswordErrorLimitExceeded, ex,
                    _headerService.GetConversationId());
            }
            catch (NotAuthorizedException ex)
            {
                return Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.ChangePasswordErrorUnauthorised, ex,
                    _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.InternalServerError, ex, _headerService.GetConversationId());
                _logger.LogError(ex,
                    $"Unknown error changing user password\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                return exceptionResponse;
            }
        }

        public async Task<Response<object>> ChangeEmailAsync(string currentEmail, string newEmail)
        {
            try
            {
                var response = await _provider.AdminUpdateUserAttributesAsync(new AdminUpdateUserAttributesRequest
                {
                    UserPoolId = _awsSettings.CognitoPoolId,
                    Username = currentEmail,
                    UserAttributes = new List<AttributeType>
                    {
                        new AttributeType
                        {
                            Name = "email",
                            Value = newEmail
                        }
                    }
                });
                if (response == null)
                {
                    return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.ChangeEmailError, "Change user email error",
                        _headerService.GetConversationId());
                }

                return IsSuccessHttpStatusCode((int)response.HttpStatusCode)
                    ? Response<object>.CreateSuccessfulResponse()
                    : Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.ChangeEmailError,
                        $"Change user email returned response code: {response.HttpStatusCode}",
                        _headerService.GetConversationId());
            }
            catch (InvalidParameterException ex)
            {
                return Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.ChangeEmailErrorInvalidParameter, ex,
                    _headerService.GetConversationId());
            }
            catch (NotAuthorizedException ex)
            {
                return Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.ChangeEmailErrorUnauthorised, ex,
                    _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.InternalServerError, ex, _headerService.GetConversationId());
                _logger.LogError(ex,
                    $"Unknown error changing user email\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                return exceptionResponse;
            }
        }

        public async Task<List<string>> ValidatePassword(string password)
        {
            var passwordPolicyTypeResponse = await GetPasswordPolicyTypeAsync();

            List<string> passwordErrors;
            if (passwordPolicyTypeResponse == null)
            {
                _logger.LogWarning($"Could not get password policy. So using default policy!");
                passwordErrors = PasswordHelper.PasswordRequirements(password).ToList();
            }
            else
            {
                passwordErrors = PasswordHelper.PasswordRequirements
                (
                    password,
                    passwordPolicyTypeResponse.MinimumLength,
                    passwordPolicyTypeResponse.RequireLowercase,
                    passwordPolicyTypeResponse.RequireNumbers,
                    passwordPolicyTypeResponse.RequireSymbols,
                    passwordPolicyTypeResponse.RequireUppercase
                ).ToList();
            }

            return passwordErrors;
        }

        private static bool IsSuccessHttpStatusCode(int httpStatusCode) => httpStatusCode >= StatusCodes.Status200OK &&
                                                                           httpStatusCode <
                                                                           StatusCodes.Status300MultipleChoices;
    }
}
