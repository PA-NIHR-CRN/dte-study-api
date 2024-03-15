using System.Globalization;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using BPOR.Domain.Settings;
using BPOR.Domain.Utils;
using BPOR.Infrastructure.Constants;
using BPOR.Infrastructure.Interfaces;
using BPOR.Infrastructure.Responses.V1.Password;
using Dte.Common;
using Dte.Common.Contracts;
using Dte.Common.Exceptions.Common;
using Dte.Common.Helpers;
using Dte.Common.Models;
using Dte.Common.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BPOR.Infrastructure.Services;

public class PasswordService(
    IOptions<AwsSettings> awsSettings,
    IAmazonCognitoIdentityProvider provider,
    IParticipantService participantService,
    IContentfulService contentfulService,
    IEmailService emailService,
    ILogger<PasswordService> logger,
    IOptions<ContentfulSettings> contentfulSettings,
    IUserService userService)
    : IPasswordService
{
    public async Task<PasswordPolicyTypeResponse> GetPasswordPolicyTypeAsync(CancellationToken cancellationToken)
    {
        try
        {
            var describeUserPoolResponse = await provider.DescribeUserPoolAsync(new DescribeUserPoolRequest
                { UserPoolId = awsSettings.Value.CognitoPoolId }, cancellationToken);

            if (describeUserPoolResponse?.UserPool?.Policies?.PasswordPolicy == null)
            {
                return null;
            }

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
            logger.LogError(ex, "Could not get Cognito password policy");

            return null;
        }
    }

    public async Task<Response<ForgotPasswordResponse>> ForgotPasswordAsync(string email,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("email: {email}", email);

        var user = await userService.AdminGetUserAsync(email, cancellationToken);

        if (user == null || !user.Enabled)
        {
            var participantDetails =
                await participantService.GetParticipantDetailsByEmailAsync(email, cancellationToken);

            if (string.IsNullOrWhiteSpace(participantDetails?.NhsId))
                return Response<ForgotPasswordResponse>.CreateSuccessfulResponse(
                );

            var request = new EmailContentRequest
            {
                EmailName = contentfulSettings.Value.EmailTemplates.NhsPasswordReset,
                SelectedLocale = new CultureInfo(participantDetails.SelectedLocale ?? SelectedLocale.Default),
                FirstName = participantDetails.Firstname,
            };

            var contentfulEmail = await contentfulService.GetEmailContentAsync(request);

            await emailService.SendEmailAsync(participantDetails.Email, contentfulEmail.EmailSubject,
                contentfulEmail.EmailBody, cancellationToken);

            return Response<ForgotPasswordResponse>.CreateSuccessfulResponse(
            );
        }

        if (user.UserStatus == UserStatusType.CONFIRMED)
        {
            var response = await provider.ForgotPasswordAsync(new ForgotPasswordRequest
            {
                ClientId = awsSettings.Value.CognitoAppClientIds[0],
                Username = email
            }, cancellationToken);

            if (!HttpUtils.IsSuccessStatusCode((int)response.HttpStatusCode))
            {
                logger.LogWarning("ForgotPasswordAsync returned: {Response}",
                    JsonConvert.SerializeObject(response));
            }
        }

        return Response<ForgotPasswordResponse>.CreateSuccessfulResponse();
    }

    public async Task<Response<ConfirmForgotPasswordResponse>> ConfirmForgotPasswordAsync(string code, string userId,
        string password, CancellationToken cancellationToken)
    {
        try
        {
            var passwordErrors = await ValidatePasswordAsync(password, cancellationToken);

            if (passwordErrors.Any())
            {
                return Response<ConfirmForgotPasswordResponse>.CreateErrorMessageResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.PasswordValidationError,
                    $"Password validation errors: {string.Join("; ", passwordErrors)}");
            }

            var response = await provider.ConfirmForgotPasswordAsync(new ConfirmForgotPasswordRequest
            {
                ClientId = awsSettings.Value.CognitoAppClientIds[0],
                ConfirmationCode = code,
                Username = userId,
                Password = password
            }, cancellationToken);

            return HttpUtils.IsSuccessStatusCode((int)response.HttpStatusCode)
                ? Response<ConfirmForgotPasswordResponse>.CreateSuccessfulResponse()
                : Response<ConfirmForgotPasswordResponse>.CreateErrorMessageResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.ConfirmForgotPasswordError,
                    $"Confirm Forgot password response returned code: {response.HttpStatusCode}");
        }
        catch (Exception ex)
        {
            var exceptionResponse = Response<ConfirmForgotPasswordResponse>.CreateExceptionResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex);
            logger.LogError(ex, "Unknown error confirming forgot password with userId {UserId}\\r\\n{SerializeObject}",
                userId, JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented));
            return exceptionResponse;
        }
    }

    public async Task<Response<object>> ChangePasswordAsync(string email, string newPassword,
        CancellationToken cancellationToken)
    {
        try
        {
            var passwordErrors = await ValidatePasswordAsync(newPassword, cancellationToken);

            if (passwordErrors.Any())
            {
                return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.PasswordValidationError,
                    $"Password validation errors: {string.Join("; ", passwordErrors)}");
            }

            var response = await provider.AdminSetUserPasswordAsync(new AdminSetUserPasswordRequest
            {
                UserPoolId = awsSettings.Value.CognitoPoolId,
                Username = email,
                Password = newPassword,
                Permanent = true
            }, cancellationToken);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                return Response<object>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.ChangePasswordError,
                    $"Change user password returned response code: {response.HttpStatusCode}");
            }

            return Response<object>.CreateSuccessfulResponse();
        }
        catch (LimitExceededException ex)
        {
            return Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                ErrorCode.ChangePasswordErrorLimitExceeded, ex);
        }
        catch (NotAuthorizedException ex)
        {
            return Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(UserService),
                ErrorCode.ChangePasswordErrorUnauthorised, ex);
        }
        catch (Exception ex)
        {
            var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.InternalServerError, ex);
            logger.LogError(ex, "Unknown error changing user password\\r\\n{SerializeObject}",
                JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented));
            return exceptionResponse;
        }
    }

    public async Task<List<string>> ValidatePasswordAsync(string password, CancellationToken cancellationToken)
    {
        var passwordPolicyTypeResponse = await GetPasswordPolicyTypeAsync(cancellationToken);

        List<string> passwordErrors;
        if (passwordPolicyTypeResponse == null)
        {
            logger.LogWarning($"Could not get password policy. So using default policy!");
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
}
