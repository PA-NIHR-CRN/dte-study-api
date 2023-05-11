using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Application.Constants;
using Application.Contracts;
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

        public UserService(IMediator mediator, IAmazonCognitoIdentityProvider provider, IHeaderService headerService,
            AwsSettings awsSettings, ILogger<UserService> logger, EmailSettings emailSettings,
            IEmailService emailService, IParticipantService participantService,
            NhsLoginHttpClient nhsLoginHttpClient, IOptions<DevSettings> devSettings)

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
                        "If you close your NHS login account, your Be Part of Research account will remain open and if you would also like to close your Be Part of Research account you will need to email <a href=\"mailto:Bepartofresearch@nihr.ac.uk\">Bepartofresearch@nihr.ac.uk</a>.")
                    .Replace("###LINK_REPLACE###", "")
                    .Replace("###LINK_DISPLAY_VALUE_REPLACE###", "block")
                    .Replace("###TEXT_REPLACE4###",
                        "Thank you for your ongoing commitment and support.")
                    .Replace("###TEXT_REPLACE5###", "");

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
                        var participant = await _participantService.GetParticipantDetails(cognitoUser.Username);
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
                            .Replace("###TEXT_REPLACE5###", "Thank you for your ongoing commitment and support.");

                        await _emailService.SendEmailAsync(email, "Be Part of Research registration attempt", htmlBody);
                    }

                    return Response<SignUpResponse>.CreateSuccessfulContentResponse(
                        new SignUpResponse { UserExists = true, }, _headerService.GetConversationId());
                }

                // check if user exists in participant details table and send email
                var participantDetails = await _participantService.GetParticipantDetailsByEmail(email);
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
                        .Replace("###TEXT_REPLACE5###", "Thank you for your ongoing commitment and support.");

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

                return IsSuccessHttpStatusCode((int)response.HttpStatusCode)
                    ? new AdminGetUserResponse
                    {
                        Email = email, Id = response.Username, Status = response.UserStatus?.ToString(),
                        CreatedDate = response.UserCreateDate, LastModifiedDate = response.UserLastModifiedDate,
                        Enabled = response.Enabled
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
                var participantDetails = await _participantService.GetParticipantDetailsByEmail(email);
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
                    .Replace("###TEXT_REPLACE5###", "");

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
}