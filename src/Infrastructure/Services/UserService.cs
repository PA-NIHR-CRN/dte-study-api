using System;
using System.Collections.Generic;
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
using Dte.Common.Exceptions;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Helpers;
using Dte.Common.Http;
using Dte.Common.Responses;
using Infrastructure.Clients;
using Infrastructure.Content;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using AdminGetUserResponse = Application.Responses.V1.Users.AdminGetUserResponse;
using ConfirmForgotPasswordResponse = Application.Responses.V1.Users.ConfirmForgotPasswordResponse;
using ForgotPasswordResponse = Application.Responses.V1.Users.ForgotPasswordResponse;
using ResendConfirmationCodeResponse = Application.Responses.V1.Users.ResendConfirmationCodeResponse;
using SignUpResponse = Application.Responses.V1.Users.SignUpResponse;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IAmazonCognitoIdentityProvider _provider;
        private readonly IHeaderService _headerService;
        private readonly AwsSettings _awsSettings;
        private readonly ILogger<UserService> _logger;
        private readonly IEmailService _emailService;
        private readonly EmailSettings _emailSettings;
        private readonly NhsLoginHttpClient _nhsLoginHttpClient;
        private readonly IMediator _mediator;
        private readonly DevSettings _devSettings;
        private readonly IParticipantService _participantService;
        private readonly IDataProtector _dataProtector;

        public UserService(IMediator mediator, IAmazonCognitoIdentityProvider provider, IHeaderService headerService,
            AwsSettings awsSettings, ILogger<UserService> logger, EmailSettings emailSettings,
            IEmailService emailService, IParticipantService participantService,
            NhsLoginHttpClient nhsLoginHttpClient, IOptions<DevSettings> devSettings,
            IDataProtectionProvider dataProtector)

        {
            _provider = provider;
            _headerService = headerService;
            _awsSettings = awsSettings;
            _logger = logger;
            _emailService = emailService;
            _emailSettings = emailSettings;
            _nhsLoginHttpClient = nhsLoginHttpClient;
            _mediator = mediator;
            _devSettings = devSettings.Value;
            _participantService = participantService;
            _dataProtector = dataProtector.CreateProtector("mfa.login.details");
        }

        private string GenerateMfaDetails(AdminInitiateAuthResponse response, string password = null)
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
                var mfaDetails = GenerateMfaDetails(response, password);

                if (response.ChallengeName == ChallengeNameType.MFA_SETUP)
                {
                    return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), "Mfa_Setup_Challenge",
                        mfaDetails, _headerService.GetConversationId());
                }

                if (response.ChallengeName == ChallengeNameType.SMS_MFA)
                {
                    return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), "Sms_Mfa_Challenge",
                        mfaDetails, _headerService.GetConversationId());
                }

                if (response.ChallengeName == ChallengeNameType.SOFTWARE_TOKEN_MFA)
                {
                    return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), "Software_Token_Mfa_Challenge",
                        mfaDetails, _headerService.GetConversationId());
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

        private MfaLoginDetails DeserializeMfaLoginDetails(string mfaDetails) =>
            JsonConvert.DeserializeObject<MfaLoginDetails>(_dataProtector.Unprotect(mfaDetails));


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

        private Response<string> HandleMfaException(Exception ex, string errorType)
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
                var mfaDetails = GenerateMfaDetails(response, mfaLoginDetails.Password);

                if (response.ChallengeName == ChallengeNameType.SMS_MFA)
                {
                    return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), "Sms_Mfa_Challenge",
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
                    MfaValidationResult.UserNotFound => CreateErrorResponse("MFA_User_Not_Found", "User not found"),
                    MfaValidationResult.CodeExpired => CreateErrorResponse("MFA_Code_Expired", "Code has expired"),
                    MfaValidationResult.CodeInvalid => CreateErrorResponse("MFA_Code_Mismatch", "Code is invalid"),
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
            // ensure the phone number is in the correct format for cognito ie +441234567890
            phoneNumber = phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
            if (!phoneNumber.StartsWith("+"))
            {
                phoneNumber = $"+44{phoneNumber.TrimStart('0')}";
            }

            return phoneNumber;
        }

        public async Task UpdateCognitoPhoneNumberAsync(string mfaDetails, string phoneNumber)
        {
            // get the username from the mfa details
            var mfaLoginDetails = DeserializeMfaLoginDetails(mfaDetails);
            var username = mfaLoginDetails.Username;

            // ensure the phone number is in the correct format for cognito ie +441234567890
            phoneNumber = CleanPhoneNumber(phoneNumber);

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

            await _provider.AdminUpdateUserAttributesAsync(request);
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
                        UserAttributeNames = new List<string> {"phone_number"}
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
                        nameof(UserService), "Sms_Mfa_Challenge",
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
        
        private async Task<Response<string>> LoginAndHandleResponse(string username, string password, string errorDetail)
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
            return await LoginAndHandleResponse(mfaLoginDetails.Username, mfaLoginDetails.Password, "Mfa_Reissue_Session");
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
                    "Software_Token_Mfa_Challenge");
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
                if(ex.Message == "Code mismatch")
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

        private static bool IsUnder18(DateTime dateOfBirth) => DateTime.Now.AddYears(-18).Date < dateOfBirth.Date;

        public async Task<Response<NhsLoginResponse>> NhsLoginAsync(string code, string redirectUrl)
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
                });

                return Response<NhsLoginResponse>.CreateSuccessfulContentResponse(response,
                    _headerService.GetConversationId());
            }
            catch (HttpServiceException ex)
            {
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

        public async Task<Response<SignUpResponse>> NhsSignUpAsync(bool consentRegistration, string token)
        {
            try
            {
                var nhsUserInfo = await _nhsLoginHttpClient.GetUserInfoAsync(token);

                await _mediator.Send(new CreateParticipantDetailsCommand("", nhsUserInfo.Email,
                    nhsUserInfo.FirstName, nhsUserInfo.LastName,
                    consentRegistration, nhsUserInfo.NhsId, nhsUserInfo.DateOfBirth.Value, nhsUserInfo.NhsNumber));

                var baseUrl = _emailSettings.WebAppBaseUrl;
                var htmlBody = EmailTemplate.GetHtmlTemplate().Replace("###TITLE_REPLACE1###",
                        "New Be Part of Research Account")
                    .Replace("###TEXT_REPLACE1###",
                        $"Thank you for registering for Be Part of Research using your NHS login or through the NHS App. You will need to use the NHS login option on the <a href=\"{baseUrl}Participants/Options\">Be Part of Research</a> website each time you access your account.")
                    .Replace("###TEXT_REPLACE2###",
                        "By signing up, you are joining our community of amazing volunteers who are helping researchers to understand more about health and care conditions. Please visit the <a href=\"https://bepartofresearch.nihr.ac.uk/taking-part/how-to-take-part\">How to take part</a> section of the website to find out about other ways to take part in health and care research.")
                    .Replace("###TEXT_REPLACE3###",
                        "Sign up to our <a href=\"https://nihr.us14.list-manage.com/subscribe?u=299dc02111e8a68172029095f&id=3b030a1027\">newsletter</a> to receive all our research news, studies you can take part in and other opportunities helping to shape health and care research from across the UK.")
                    .Replace("###TEXT_REPLACE4###",
                        "If you close your NHS login account, your Be Part of Research account will remain open and if you would also like to close your Be Part of Research account you will need to email <a href=\"mailto:Bepartofresearch@nihr.ac.uk\">Bepartofresearch@nihr.ac.uk</a>.")
                    .Replace("###LINK_REPLACE###", "")
                    .Replace("###LINK_DISPLAY_VALUE_REPLACE###", "block")
                    .Replace("###TEXT_REPLACE5###",
                        "Thank you for your ongoing commitment and support.")
                    .Replace("###TEXT_REPLACE6###", "");

                await _emailService.SendEmailAsync(nhsUserInfo.Email, "Be Part of Research", htmlBody);

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

        public async Task<Response<SignUpResponse>> SignUpAsync(string email, string password)
        {
            try
            {
                var passwordErrors = await ValidatePassword(password);

                if (passwordErrors.Any())
                {
                    return Response<SignUpResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.PasswordValidationError,
                        $"Password validation errors: {string.Join("; ", passwordErrors)}",
                        _headerService.GetConversationId());
                }

                bool cognitoUserExists = await UserExistsAsync(email);

                if (cognitoUserExists)
                {
                    _logger.LogInformation("Attempted to create user with email {Email} but user already exists",
                        email);
                    var cognitoUser = await _provider.AdminGetUserAsync(new AdminGetUserRequest
                        { UserPoolId = _awsSettings.CognitoPoolId, Username = email });


                    // if user is not verified, resend confirmation code
                    if (cognitoUser.UserStatus == UserStatusType.UNCONFIRMED)
                    {
                        var resendConfirmationCodeResponse = await ResendVerificationEmailAsync(email);

                        if (!resendConfirmationCodeResponse.IsSuccess)
                        {
                            return Response<SignUpResponse>.CreateErrorMessageResponse(
                                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.SignUpError,
                                $"Error resending confirmation code to user with email {email}",
                                _headerService.GetConversationId());
                        }
                    }
                    else
                    {
                        // get participant details using id
                        var participant = await _participantService.GetParticipantDetailsAsync(cognitoUser.Username);
                        if (participant == null)
                        {
                            return Response<SignUpResponse>.CreateErrorMessageResponse(
                                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.SignUpError,
                                $"User with email {email} not found", _headerService.GetConversationId());
                        }

                        var baseUrl = _emailSettings.WebAppBaseUrl;
                        var htmlBody = EmailTemplate.GetHtmlTemplate().Replace("###TITLE_REPLACE1###",
                                "Be Part of Research registration attempt")
                            .Replace("###TEXT_REPLACE1###",
                                $"Hi {System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(participant.Firstname.ToLower())},")
                            .Replace("###TEXT_REPLACE2###",
                                "An attempt has been made to register again for Be Part of Research using your email address. As you already have an account with us, there is no need to re-register.")
                            .Replace("###TEXT_REPLACE3###",
                                "If you need to access your account please sign in using the link below. If you cannot remember your password, you can choose to reset it from the sign-in page.")
                            .Replace("###LINK_REPLACE###", $"{baseUrl}UserLogin")
                            .Replace("###LINK_DISPLAY_VALUE_REPLACE###", "block")
                            .Replace("###TEXT_REPLACE4###",
                                "If you did not attempt to re-register please ignore this email.")
                            .Replace("###TEXT_REPLACE5###", "Thank you for your ongoing commitment and support.")
                            .Replace("###TEXT_REPLACE6###", "");

                        await _emailService.SendEmailAsync(email, "Be Part of Research registration attempt", htmlBody);
                    }

                    return Response<SignUpResponse>.CreateSuccessfulContentResponse(
                        new SignUpResponse { UserExists = true, }, _headerService.GetConversationId());
                }

                // check if user exists in participant details table and send email
                var participantDetails = await _participantService.GetParticipantDetailsByEmailAsync(email);
                if (participantDetails != null)
                {
                    var baseUrl = _emailSettings.WebAppBaseUrl;
                    var htmlBody = EmailTemplate.GetHtmlTemplate().Replace("###TITLE_REPLACE1###",
                            "Be Part of Research registration attempt")
                        .Replace("###TEXT_REPLACE1###",
                            $"Hi {System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(participantDetails.Firstname.ToLower())},")
                        .Replace("###TEXT_REPLACE2###",
                            "An attempt to log into your Be Part of Research account using this email address has been made. As you created your account using your NHS login information, you need to use that option to login to your Be Part of Research account. ")
                        .Replace("###TEXT_REPLACE3###",
                            "Please use the link below and select the NHS login button to continue.")
                        .Replace("###LINK_REPLACE###",
                            $"<a href=\"{baseUrl}participants/options\">{baseUrl}participants/options</a>")
                        .Replace("###LINK_DISPLAY_VALUE_REPLACE###", "block")
                        .Replace("###TEXT_REPLACE4###",
                            "If you did not attempt to re-register please ignore this email.")
                        .Replace("###TEXT_REPLACE5###", "Thank you for your ongoing commitment and support.")
                        .Replace("###TEXT_REPLACE6###", "");

                    await _emailService.SendEmailAsync(email, "Be Part of Research registration attempt", htmlBody);

                    return Response<SignUpResponse>.CreateSuccessfulContentResponse(
                        new SignUpResponse { UserExists = true, }, _headerService.GetConversationId());
                }

                var response = await _provider.SignUpAsync(new SignUpRequest
                {
                    ClientId = _awsSettings.CognitoAppClientIds[0],
                    Username = email,
                    Password = password,
                });

                if (_devSettings.AutoConfirmNewCognitoSignup)
                {
                    await _provider.AdminConfirmSignUpAsync(new AdminConfirmSignUpRequest
                    {
                        Username = email,
                        UserPoolId = _awsSettings.CognitoPoolId,
                    });
                }

                return IsSuccessHttpStatusCode((int)response.HttpStatusCode)
                    ? Response<SignUpResponse>.CreateSuccessfulContentResponse(
                        new SignUpResponse { UserId = response.UserSub, }, _headerService.GetConversationId())
                    : Response<SignUpResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.SignUpError, "Signup error", _headerService.GetConversationId());
            }
            catch (UsernameExistsException ex)
            {
                var exceptionResponse = Response<SignUpResponse>.CreateExceptionResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.SignUpErrorUsernameExists, ex,
                    _headerService.GetConversationId());
                _logger.LogError(ex,
                    $"Error signing up user {email}, username already exists\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                return exceptionResponse;
            }
            catch (InvalidParameterException ex)
            {
                return Response<SignUpResponse>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.SignUpErrorInvalidParameter, ex, _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<SignUpResponse>.CreateExceptionResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                    _headerService.GetConversationId());
                _logger.LogError(ex,
                    $"Unknown error signing up user with email {email}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                return exceptionResponse;
            }
        }

        public async Task<Response<object>> ConfirmSignUpAsync(string code, string userId)
        {
            try
            {
                var getUserResponse = await AdminGetUserAsync(userId);

                if (getUserResponse == null)
                {
                    return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.ConfirmSignUpErrorUserNotFound, "User not found",
                        _headerService.GetConversationId());
                }

                if (getUserResponse.Status == UserStatusType.CONFIRMED)
                {
                    return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.ConfirmSignUpErrorUserAlreadyConfirmed,
                        "User email is already confirmed", _headerService.GetConversationId());
                }

                var response = await _provider.ConfirmSignUpAsync(new ConfirmSignUpRequest
                {
                    ClientId = _awsSettings.CognitoAppClientIds[0],
                    ConfirmationCode = code,
                    Username = userId
                });

                return IsSuccessHttpStatusCode((int)response.HttpStatusCode)
                    ? Response<object>.CreateSuccessfulResponse()
                    : Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.ConfirmSignUpError,
                        $"Confirm SignUp user returned response code: {response.HttpStatusCode}",
                        _headerService.GetConversationId());
            }
            catch (ExpiredCodeException ex)
            {
                return Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.ConfirmSignUpErrorExpiredCode, ex,
                    _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.InternalServerError, ex, _headerService.GetConversationId());
                _logger.LogError(ex,
                    $"Unknown error confirming user signup with userId {userId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                return exceptionResponse;
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
                    AllowedPasswordSymbols = string.Join(" ", PasswordHelper.SymbolList)
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

                if (getUserResponse.Status == UserStatusType.CONFIRMED)
                {
                    return Response<ResendConfirmationCodeResponse>.CreateErrorMessageResponse(
                        ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                        ErrorCode.ResendVerificationEmailErrorUserAlreadyConfirmed,
                        $"User {userId} is already confirmed", _headerService.GetConversationId());
                }

                var response = await _provider.ResendConfirmationCodeAsync(new ResendConfirmationCodeRequest
                    { Username = userId, ClientId = _awsSettings.CognitoAppClientIds[0] });

                return IsSuccessHttpStatusCode((int)response.HttpStatusCode)
                    ? Response<ResendConfirmationCodeResponse>.CreateSuccessfulResponse(
                        _headerService.GetConversationId())
                    : Response<ResendConfirmationCodeResponse>.CreateErrorMessageResponse(
                        ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                        ErrorCode.ResendVerificationEmailError,
                        $"Resend verification email response returned code: {response.HttpStatusCode}",
                        _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<ResendConfirmationCodeResponse>.CreateExceptionResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                    _headerService.GetConversationId());
                _logger.LogError(ex,
                    $"Unknown error resending verification email for userId {userId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");

                return exceptionResponse;
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
                var baseUrl = _emailSettings.WebAppBaseUrl;
                var htmlBody = EmailTemplate.GetHtmlTemplate().Replace("###TITLE_REPLACE1###",
                        "Password Reset Attempt")
                    .Replace("###TEXT_REPLACE1###",
                        "A request has been received to change the password for your Be Part of Research account. We were unable to complete this request because your account was created with your NHS login information. You will need to use this option on each occasion to access your account and update your details.")
                    .Replace("###TEXT_REPLACE2###",
                        "Please use the link below and select the NHS login button to continue.")
                    .Replace("###TEXT_REPLACE3###",
                        "")
                    .Replace("###LINK_REPLACE###",
                        $"<a href=\"{baseUrl}participants/options\">{baseUrl}participants/options</a>")
                    .Replace("###LINK_DISPLAY_VALUE_REPLACE###", "block")
                    .Replace("###TEXT_REPLACE4###",
                        "If you have not attempted to reset your password, please contact us by email at <a href=\"mailto:Bepartofresearch@nihr.ac.uk\">Bepartofresearch@nihr.ac.uk</a>")
                    .Replace("###TEXT_REPLACE5###", "")
                    .Replace("###TEXT_REPLACE6###", "");

                await _emailService.SendEmailAsync(email, "Be Part of Research password reset", htmlBody);

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

        public async Task<Response<object>> ChangePasswordAsync(string email, string oldPassword,
            string newPassword)
        {
            var request = new AdminInitiateAuthRequest
            {
                UserPoolId = _awsSettings.CognitoPoolId,
                ClientId = _awsSettings.CognitoAppClientIds[0],
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
            };

            request.AuthParameters.Add("USERNAME", email);
            request.AuthParameters.Add("PASSWORD", oldPassword);

            try
            {
                var tokenResponse = await _provider.AdminInitiateAuthAsync(request);

                if (tokenResponse?.AuthenticationResult == null)
                {
                    return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.ChangePasswordError, "Change user password error",
                        _headerService.GetConversationId());
                }

                var response = await _provider.ChangePasswordAsync(new ChangePasswordRequest
                {
                    AccessToken = tokenResponse.AuthenticationResult.AccessToken,
                    PreviousPassword = oldPassword,
                    ProposedPassword = newPassword
                });

                if (response == null)
                {
                    return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.ChangePasswordError, "Change user password error",
                        _headerService.GetConversationId());
                }

                return IsSuccessHttpStatusCode((int)response.HttpStatusCode)
                    ? Response<object>.CreateSuccessfulResponse()
                    : Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.ChangePasswordError,
                        $"Change user password returned response code: {response.HttpStatusCode}",
                        _headerService.GetConversationId());
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

        private async Task<List<string>> ValidatePassword(string password)
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

    public class MfaLoginDetails
    {
        public string Username { get; set; }
        public string SessionId { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}