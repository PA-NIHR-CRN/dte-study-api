using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Application.Constants;
using Application.Contracts;
using Application.Settings;
using Application.Content;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using AdminGetUserResponse = Application.Responses.V1.Users.AdminGetUserResponse;
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
        private readonly IMfaService _mfaService;
        private readonly IPasswordService _passwordService;
        private readonly DevSettings _devSettings;
        private readonly IParticipantService _participantService;

        public UserService(IAmazonCognitoIdentityProvider provider, IHeaderService headerService,
            AwsSettings awsSettings, ILogger<UserService> logger, EmailSettings emailSettings,
            IEmailService emailService, IParticipantService participantService, IOptions<DevSettings> devSettings,
            IMfaService mfaService,
            IPasswordService passwordService)

        {
            _provider = provider;
            _headerService = headerService;
            _awsSettings = awsSettings;
            _logger = logger;
            _emailService = emailService;
            _emailSettings = emailSettings;
            _mfaService = mfaService;
            _passwordService = passwordService;
            _devSettings = devSettings.Value;
            _participantService = participantService;
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
                var mfaDetails = _mfaService.GenerateMfaDetails(response, password);

                if (response.ChallengeName == ChallengeNameType.MFA_SETUP)
                {
                    return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.MfaSetupChallenge,
                        mfaDetails, _headerService.GetConversationId());
                }

                if (response.ChallengeName == ChallengeNameType.SMS_MFA)
                {
                    return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.MfaSmsChallenge,
                        mfaDetails, _headerService.GetConversationId());
                }

                if (response.ChallengeName == ChallengeNameType.SOFTWARE_TOKEN_MFA)
                {
                    return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.MfaSoftwareTokenChallenge,
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

        public async Task<Response<SignUpResponse>> SignUpAsync(string email, string password)
        {
            try
            {
                var passwordErrors = await _passwordService.ValidatePassword(password);

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
                        new SignUpResponse { IsSuccess = true, }, _headerService.GetConversationId());
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
                        new SignUpResponse { IsSuccess = true, }, _headerService.GetConversationId());
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

                return Response<SignUpResponse>.CreateSuccessfulContentResponse(
                    new SignUpResponse { IsSuccess = true, UserId = response.UserSub },
                    _headerService.GetConversationId());
            }
            catch (UsernameExistsException ex)
            {
                _logger.LogError(ex,
                    $"Error signing up user {email}, username already exists");
                // Return a generic success response, to appear as though registration was successful
                return Response<SignUpResponse>.CreateSuccessfulContentResponse(
                    new SignUpResponse { IsSuccess = true }, _headerService.GetConversationId());
            }
            catch (InvalidParameterException ex)
            {
                _logger.LogError(ex,
                    $"Invalid parameters provided for user signup {email}");
                return Response<SignUpResponse>.CreateErrorMessageResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                    ErrorCode.SignUpError, "An error occurred during sign up. Please try again later.",
                    _headerService.GetConversationId());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Unknown error signing up user with email {email}");
                return Response<SignUpResponse>.CreateErrorMessageResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                    ErrorCode.InternalServerError, "An error occurred during sign up. Please try again later.",
                    _headerService.GetConversationId());
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

                if (!HttpStatusCodeHelper.IsSuccess(response.HttpStatusCode))
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

                var passwordErrors = await _passwordService.ValidatePassword(password);

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

                if (!HttpStatusCodeHelper.IsSuccess(createResponse.HttpStatusCode))
                {
                    return Response<SignUpResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UserService), ErrorCode.AdminCreateUserError, "Create user error",
                        _headerService.GetConversationId());
                }

                var setPasswordResponse = await _provider.AdminSetUserPasswordAsync(new AdminSetUserPasswordRequest
                {
                    UserPoolId = _awsSettings.CognitoPoolId, Username = email, Password = password, Permanent = true
                });

                return HttpStatusCodeHelper.IsSuccess(setPasswordResponse.HttpStatusCode)
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

                return HttpStatusCodeHelper.IsSuccess(response.HttpStatusCode)
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
                    if (!HttpStatusCodeHelper.IsSuccess(response.HttpStatusCode))
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

                return HttpStatusCodeHelper.IsSuccess(response.HttpStatusCode)
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

                return HttpStatusCodeHelper.IsSuccess(response.HttpStatusCode)
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

        private async Task<bool> UserExistsAsync(string email)
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
    }
}
