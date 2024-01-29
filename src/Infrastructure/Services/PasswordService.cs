using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Application.Constants;
using Application.Content;
using Application.Contracts;
using Application.Responses.V1.Users;
using Application.Settings;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Helpers;
using Dte.Common.Http;
using Dte.Common.Responses;
using Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ConfirmForgotPasswordResponse = Application.Responses.V1.Users.ConfirmForgotPasswordResponse;
using ForgotPasswordResponse = Application.Responses.V1.Users.ForgotPasswordResponse;

namespace Infrastructure.Services;

public class PasswordService : IPasswordService
{
    private readonly AwsSettings _awsSettings;
    private readonly IAmazonCognitoIdentityProvider _provider;
    private readonly IHeaderService _headerService;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly IParticipantService _participantService;
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<PasswordService> _logger;

    public PasswordService(ILogger<PasswordService> logger, AwsSettings awsSettings,
        IAmazonCognitoIdentityProvider provider, IHeaderService headerService, IUserService userService,
        IEmailService emailService, IParticipantService participantService, EmailSettings emailSettings)

    {
        _awsSettings = awsSettings;
        _provider = provider;
        _headerService = headerService;
        _userService = userService;
        _emailService = emailService;
        _participantService = participantService;
        _emailSettings = emailSettings;
        _logger = logger;
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

    public async Task<Response<ForgotPasswordResponse>> ForgotPasswordAsync(string email)
    {
        _logger.LogInformation("email: {email}", email);

        var user = await _userService.AdminGetUserAsync(email);

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

            if (!HttpStatusCodeHelper.IsSuccess(response.HttpStatusCode))
            {
                _logger.LogWarning("ForgotPasswordAsync returned: {Response}",
                    JsonConvert.SerializeObject(response));
            }
        }

        return Response<ForgotPasswordResponse>.CreateSuccessfulResponse(_headerService.GetConversationId());
    }

    public async Task<Response<ConfirmForgotPasswordResponse>> ConfirmForgotPasswordAsync(string code, string userId,
        string password)
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

            return HttpStatusCodeHelper.IsSuccess(response.HttpStatusCode)
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
            _logger.LogError(ex, "Unknown error confirming forgot password with userId {UserId}\\r\\n{SerializeObject}", userId, JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented));
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
}
