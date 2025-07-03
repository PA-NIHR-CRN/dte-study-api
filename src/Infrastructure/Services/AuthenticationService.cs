using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Application.Constants;
using Application.Contracts;
using Application.Models.MFA;
using Application.Settings;
using Dte.Common;
using Dte.Common.Contracts;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Models;
using Dte.Common.Responses;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using SignUpResponse = Application.Responses.V1.Users.SignUpResponse;

namespace Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAmazonCognitoIdentityProvider _provider;
    private readonly IHeaderService _headerService;
    private readonly AwsSettings _awsSettings;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IEmailService _emailService;
    private readonly IParticipantService _participantService;
    private readonly IContentfulService _contentfulService;
    private readonly ContentfulSettings _contentfulSettings;
    private readonly IUserService _userService;
    private readonly IDataProtector _dataProtector;

    public AuthenticationService(IAmazonCognitoIdentityProvider provider, IHeaderService headerService,
        AwsSettings awsSettings, ILogger<AuthenticationService> logger, IEmailService emailService,
        IParticipantService participantService, IContentfulService contentfulService,
        ContentfulSettings contentfulSettings, IUserService userService, IDataProtectionProvider dataProtector)

    {
        _provider = provider;
        _headerService = headerService;
        _awsSettings = awsSettings;
        _logger = logger;
        _emailService = emailService;
        _participantService = participantService;
        _contentfulService = contentfulService;
        _contentfulSettings = contentfulSettings;
        _userService = userService;
        _dataProtector = dataProtector.CreateProtector("mfa.login.details");
    }

    public async Task<Response<SignUpResponse>> SignUpAsync(string email, string password, string selectedLocale)
    {
        try
        {
            var passwordErrors = await _userService.ValidatePassword(password);

            if (passwordErrors.Any())
            {
                return Response<SignUpResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.PasswordValidationError,
                    $"Password validation errors: {string.Join("; ", passwordErrors)}",
                    _headerService.GetConversationId());
            }

            bool cognitoUserExists = await _userService.UserExistsAsync(email);

            if (cognitoUserExists)
            {
                _logger.LogInformation("Attempted to create user with email {Email} but user already exists",
                    email);
                var cognitoUser = await _provider.AdminGetUserAsync(new AdminGetUserRequest
                    { UserPoolId = _awsSettings.CognitoPoolId, Username = email });


                // if user is not verified, resend confirmation code
                if (cognitoUser.UserStatus == UserStatusType.UNCONFIRMED)
                {
                    var resendConfirmationCodeResponse = await _userService.ResendVerificationEmailAsync(email);

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

                    var request = new EmailContentRequest
                    {
                        EmailName = _contentfulSettings.EmailTemplates.EmailAccountExists,
                        SelectedLocale = new CultureInfo(participant.SelectedLocale ?? SelectedLocale.Default),
                        FirstName = participant.Firstname,
                    };

                    var contentfulEmail = await _contentfulService.GetEmailContentAsync(request);

                    await _emailService.SendEmailAsync(participant.Email, contentfulEmail.EmailSubject,
                        contentfulEmail.EmailBody);

                    throw new UsernameExistsException(email);
                }
            }


            // check if user exists in participant details table and send email
            var participantDetails = await _participantService.GetParticipantDetailsByEmailAsync(email);
            if (participantDetails != null)
            {
                var request = new EmailContentRequest
                {
                    EmailName = _contentfulSettings.EmailTemplates.NhsAccountExists,
                    SelectedLocale = new CultureInfo(participantDetails.SelectedLocale ?? SelectedLocale.Default),
                    FirstName = participantDetails.Firstname,
                };

                var contentfulEmail = await _contentfulService.GetEmailContentAsync(request);

                await _emailService.SendEmailAsync(participantDetails.Email, contentfulEmail.EmailSubject,
                    contentfulEmail.EmailBody);

                throw new UsernameExistsException(email);
            }

            var response = await _provider.SignUpAsync(new SignUpRequest
            {
                ClientId = _awsSettings.CognitoAppClientIds[0],
                Username = email,
                Password = password,
                ClientMetadata = new Dictionary<string, string>
                {
                    { "selectedLocale", selectedLocale ?? "en-GB" }
                }
            });

            return Response<SignUpResponse>.CreateSuccessfulContentResponse(
                new SignUpResponse { IsSuccess = true, UserId = response.UserSub },
                _headerService.GetConversationId());
        }
        catch (UsernameExistsException ex)
        {
            _logger.LogError(ex, "Error signing up user {Email}, username already exists", email);
            // Return a generic success response, to appear as though registration was successful
            return Response<SignUpResponse>.CreateSuccessfulContentResponse(
               new SignUpResponse { IsSuccess = true },
               _headerService.GetConversationId());
        }

        catch (InvalidParameterException ex)
        {
            _logger.LogError(ex, "Invalid parameters provided for user signup {Email}", email);
            return Response<SignUpResponse>.CreateErrorMessageResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                ErrorCode.SignUpError, "An error occurred during sign up. Please try again later.",
                _headerService.GetConversationId());
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Unknown error signing up user with email {Email}", email);
            return Response<SignUpResponse>.CreateErrorMessageResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                ErrorCode.InternalServerError, "An error occurred during sign up. Please try again later.",
                _headerService.GetConversationId());
        }
    }

    public async Task<Response<string>> RespondToMfaChallengeAsync(string mfaCode, string mfaDetails)
    {
        try
        {
            var mfaLoginDetails = MfaLoginDetails.FromProtectedString(_dataProtector, mfaDetails);
            _logger.LogInformation("{mfaDetails}", new { mfaLoginDetails.Username, mfaLoginDetails.SessionId });

            var request = _userService.CreateAuthChallengeRequest("SMS_MFA", mfaLoginDetails.SessionId,
                mfaLoginDetails.Username,
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
            return _userService.HandleMfaException(ex, ErrorCode.MfaCodeMismatch);
        }
        catch (NotAuthorizedException ex)
        {
            return _userService.HandleMfaException(ex,
                ex.Message == "Invalid session for the user, session is expired."
                    ? ErrorCode.MfaSessionExpired
                    : "Not_Authorized");
        }
        catch (Exception ex)
        {
            var response = _userService.HandleMfaException(ex, "Unknown error responding to mfa challenge");
            return response;
        }
    }
}
