using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using BPOR.Domain.Settings;
using BPOR.Infrastructure.Constants;
using BPOR.Infrastructure.Interfaces;
using BPOR.Infrastructure.Models.Mfa;
using Dte.Common.Exceptions.Common;
using Dte.Common.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BPOR.Infrastructure.Services;

public class AuthService(
    IHttpContextAccessor httpContextAccessor,
    ISessionService sessionService,
    ILogger<AuthService> logger,
    IOptions<AwsSettings> awsSettings,
    IAmazonCognitoIdentityProvider provider,
    IDataProtector dataProtector)
    : IAuthService
{
    public async Task<Response<string>> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var request = new AdminInitiateAuthRequest
        {
            UserPoolId = awsSettings.Value.CognitoPoolId,
            ClientId = awsSettings.Value.CognitoAppClientIds[0],
            AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
        };

        request.AuthParameters.Add("USERNAME", email);
        request.AuthParameters.Add("PASSWORD", password);
        // can't get custom scopes from this call, you can only get it from a url call like the hosted UI does.. (https://github.com/aws/aws-sdk-net-extensions-cognito/issues/42)

        try
        {
            var response = await provider.AdminInitiateAuthAsync(request, cancellationToken);
            var protectedString = MfaLoginDetails.ToProtectedString(dataProtector, response, password);

            if (response.ChallengeName == ChallengeNameType.MFA_SETUP)
            {
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.MfaSetupChallenge,
                    protectedString);
            }

            if (response.ChallengeName == ChallengeNameType.SMS_MFA)
            {
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.MfaSmsChallenge,
                    protectedString);
            }

            if (response.ChallengeName == ChallengeNameType.SOFTWARE_TOKEN_MFA)
            {
                return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.MfaSoftwareTokenChallenge,
                    protectedString);
            }

            if (response?.AuthenticationResult != null)
                return Response<string>.CreateSuccessfulContentResponse(response.AuthenticationResult.IdToken);

            logger.LogError("AWS Cognito returned as response without an AuthenticationResult");
            return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.AuthenticationError,
                "Authentication Result from the service provider was null");
        }
        catch (UserNotFoundException)
        {
            return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.AuthenticationNotAuthorized, "");
        }
        catch (NotAuthorizedException)
        {
            return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.AuthenticationNotAuthorized, "");
        }
        catch (UserNotConfirmedException)
        {
            return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.AuthenticationNotAuthorized, "");
        }
        catch (LimitExceededException)
        {
            return Response<string>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.AuthenticationNotAuthorized, "");
        }
        catch (Exception ex)
        {
            var exceptionResponse = Response<string>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName,
                nameof(UserService), ErrorCode.InternalServerError, ex);
            logger.LogError(ex, "Unknown error logging in user with email {Email}\\r\\n{SerializeObject}", email,
                JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented));
            return exceptionResponse;
        }
    }


    public async Task CreateSessionAndLoginAsync(string jwtToken, string sessionId, CancellationToken cancellationToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);

        var claims = new List<Claim>
        {
            new Claim("sessionId", sessionId)
        };
        claims.AddRange(token.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
        };

        await httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        var participantId = token.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

        await sessionService.SetSessionAsync(participantId, sessionId, cancellationToken);
    }
}
