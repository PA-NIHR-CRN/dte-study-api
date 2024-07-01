using System.Globalization;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using BPOR.Domain.Settings;
using BPOR.Infrastructure.Constants;
using BPOR.Infrastructure.Interfaces;
using Dte.Common;
using Dte.Common.Contracts;
using Dte.Common.Exceptions.Common;
using Dte.Common.Models;
using Dte.Common.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Interfaces;
using SignUpResponse = BPOR.Infrastructure.Responses.V1.Users.SignUpResponse;

namespace BPOR.Infrastructure.Services;

public class SignUpService(
    IPasswordService passwordService,
    IAmazonCognitoIdentityProvider provider,
    IParticipantService participantService,
    IContentfulService contentfulService,
    IEmailService emailService,
    ILogger<SignUpService> logger,
    IOptions<ContentfulSettings> contentfulSettings,
    IUserService userService,
    IOptions<AwsSettings> awsSettings)
    : ISignUpService
{
    public async Task<Response<SignUpResponse>> SignUpAsync(string email, string password, string selectedLocale,
        CancellationToken cancellationToken)
    {
        try
        {
            var passwordErrors = await passwordService.ValidatePasswordAsync(password, cancellationToken);

            if (passwordErrors.Any())
            {
                return Response<SignUpResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.PasswordValidationError,
                    $"Password validation errors: {string.Join("; ", passwordErrors)}"
                );
            }

            bool cognitoUserExists = await userService.UserExistsAsync(email, cancellationToken);

            if (cognitoUserExists)
            {
                logger.LogInformation("Attempted to create user with email {Email} but user already exists",
                    email);
                var cognitoUser = await provider.AdminGetUserAsync(new AdminGetUserRequest
                    { UserPoolId = awsSettings.Value.CognitoPoolId, Username = email }, cancellationToken);


                // if user is not verified, resend confirmation code
                if (cognitoUser.UserStatus == UserStatusType.UNCONFIRMED)
                {
                    var resendConfirmationCodeResponse =
                        await userService.ResendVerificationEmailAsync(email, cancellationToken);

                    if (!resendConfirmationCodeResponse.IsSuccess)
                    {
                        return Response<SignUpResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                            nameof(UserService), ErrorCode.SignUpError,
                            $"Error resending confirmation code to user with email {email}");
                    }
                }
                else
                {
                    // get participant details using id
                    var participant =
                        await participantService.GetParticipantAsync(cognitoUser.Username, cancellationToken);
                    if (participant == null)
                    {
                        return Response<SignUpResponse>.CreateErrorMessageResponse(
                            ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.SignUpError,
                            $"User with email {email} not found");
                    }

                    var request = new EmailContentRequest
                    {
                        EmailName = contentfulSettings.Value.EmailTemplates.EmailAccountExists,
                        SelectedLocale = new CultureInfo(participant.SelectedLocale ?? SelectedLocale.Default),
                        FirstName = participant.Firstname,
                    };

                    var contentfulEmail = await contentfulService.GetEmailContentAsync(request);

                    await emailService.SendEmailAsync(participant.Email, contentfulEmail.EmailSubject,
                        contentfulEmail.EmailBody, cancellationToken);

                    throw new UsernameExistsException(email);
                }
            }


            // check if user exists in participant details table and send email
            var participantDetails =
                await participantService.GetParticipantDetailsByEmailAsync(email, cancellationToken);
            if (participantDetails != null)
            {
                var request = new EmailContentRequest
                {
                    EmailName = contentfulSettings.Value.EmailTemplates.NhsAccountExists,
                    SelectedLocale = new CultureInfo(participantDetails.SelectedLocale ?? SelectedLocale.Default),
                    FirstName = participantDetails.Firstname,
                };

                var contentfulEmail = await contentfulService.GetEmailContentAsync(request);

                await emailService.SendEmailAsync(participantDetails.Email, contentfulEmail.EmailSubject,
                    contentfulEmail.EmailBody, cancellationToken);

                throw new UsernameExistsException(email);
            }

            var response = await provider.SignUpAsync(new SignUpRequest
            {
                ClientId = awsSettings.Value.CognitoAppClientIds[0],
                Username = email,
                Password = password,
                ClientMetadata = new Dictionary<string, string>
                {
                    { "selectedLocale", selectedLocale ?? "en-GB" }
                }
            }, cancellationToken);

            return Response<SignUpResponse>.CreateSuccessfulContentResponse(new SignUpResponse
                { IsSuccess = true, UserId = response.UserSub });
        }
        catch (UsernameExistsException ex)
        {
            logger.LogError(ex, "Error signing up user {Email}, username already exists", email);
            // Return a generic success response, to appear as though registration was successful
            return Response<SignUpResponse>.CreateErrorMessageResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                ErrorCode.SignUpError, "An error occurred during sign up. Please try again later.");
        }
        catch (InvalidParameterException ex)
        {
            logger.LogError(ex, "Invalid parameters provided for user signup {Email}", email);
            return Response<SignUpResponse>.CreateErrorMessageResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                ErrorCode.SignUpError, "An error occurred during sign up. Please try again later.");
        }
    }
}
