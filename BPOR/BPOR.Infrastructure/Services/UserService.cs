using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using BPOR.Domain.Settings;
using BPOR.Infrastructure.Constants;
using BPOR.Infrastructure.Interfaces;
using Dte.Common.Exceptions.Common;
using Dte.Common.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using BPOR.Domain.Utils;

namespace BPOR.Infrastructure.Services;

public class UserService(
    IAmazonCognitoIdentityProvider provider,
    IOptions<AwsSettings> awsSettings,
    ILogger<UserService> logger,
    IParticipantService participantService)
    : IUserService
{
    public async Task<Response<ResendConfirmationCodeResponse>> ResendVerificationEmailAsync(string userId,
        CancellationToken cancellationToken)
    {
        var getUserResponse = await AdminGetUserAsync(userId, cancellationToken);

        if (getUserResponse.UserStatus != UserStatusType.CONFIRMED)
        {
            var response = await provider.ResendConfirmationCodeAsync(new ResendConfirmationCodeRequest
            {
                Username = userId,
                ClientId = awsSettings.Value.CognitoAppClientIds[0]
            }, cancellationToken);

            // Log the response for internal tracking but do not return specifics to the client
            if (!HttpUtils.IsSuccessStatusCode((int)response.HttpStatusCode))
            {
                logger.LogError(
                    "Resend verification email response returned code: {ResponseHttpStatusCode} for userId {UserId}",
                    response.HttpStatusCode, userId);
            }
        }

        // Always return the same generic response regardless of user state or other conditions
        return Response<ResendConfirmationCodeResponse>.CreateSuccessfulResponse();
    }

    public async Task<object?> ConfirmSignUpAsync(string code, string userId, CancellationToken cancellationToken)
    {
        try
        {
            var getUserResponse = await AdminGetUserAsync(userId, cancellationToken);

            if (getUserResponse == null || getUserResponse.UserStatus == UserStatusType.CONFIRMED)
            {
                logger.LogInformation("User {UserId} not found or already confirmed", userId);
                return Response<object>.CreateSuccessfulResponse();
            }

            var response = await provider.ConfirmSignUpAsync(new ConfirmSignUpRequest
            {
                ClientId = awsSettings.Value.CognitoAppClientIds[0],
                ConfirmationCode = code,
                Username = userId
            }, cancellationToken);

            if (!HttpUtils.IsSuccessStatusCode((int)response.HttpStatusCode))
            {
                logger.LogError("Confirm SignUp user returned response code: {ResponseHttpStatusCode}",
                    response.HttpStatusCode);
            }

            // Return a generic success message regardless of the outcome.
            return Response<object>.CreateSuccessfulResponse();
        }
        catch (ExpiredCodeException ex)
        {
            logger.LogError(ex, "Expired code error during confirmation for user {UserId}", userId);
            return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.InternalServerError,
                "An error occurred during confirmation. Please try again.");
        }
    }

    public async Task<Response<object>> ChangeEmailAsync(string currentEmail, string newEmail,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await provider.AdminUpdateUserAttributesAsync(new AdminUpdateUserAttributesRequest
            {
                UserPoolId = awsSettings.Value.CognitoPoolId,
                Username = currentEmail,
                UserAttributes = new List<AttributeType>
                {
                    new AttributeType
                    {
                        Name = "email",
                        Value = newEmail
                    }
                }
            }, cancellationToken);
            if (response == null)
            {
                return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.ChangeEmailError, "Change user email error");
            }

            if (HttpUtils.IsSuccessStatusCode((int)response.HttpStatusCode))
            {
                var participant =
                    await participantService.GetParticipantDetailsByEmailAsync(currentEmail, cancellationToken);
                if (participant != null)
                {
                    participant.Email = newEmail;
                    await participantService.UpdateParticipantAsync(participant, cancellationToken);
                }
            }

            return Response<object>.CreateSuccessfulResponse();
        }
        catch (InvalidParameterException ex)
        {
            return Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                ErrorCode.ChangeEmailErrorInvalidParameter, ex);
        }
        catch (NotAuthorizedException ex)
        {
            return Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                ErrorCode.ChangeEmailErrorUnauthorised, ex);
        }
    }

    public async Task<AdminGetUserResponse> AdminGetUserAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            return await provider.AdminGetUserAsync(new AdminGetUserRequest
            {
                UserPoolId = awsSettings.Value.CognitoPoolId,
                Username = email
            }, cancellationToken);
        }
        catch (UserNotFoundException)
        {
            return null;
        }
    }

    public async Task<bool> UserExistsAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            var response = await provider.AdminGetUserAsync(new AdminGetUserRequest
                { UserPoolId = awsSettings.Value.CognitoPoolId, Username = email }, cancellationToken);

            return response is { HttpStatusCode: HttpStatusCode.OK };
        }
        catch (UserNotFoundException)
        {
            return false;
        }
    }
}
