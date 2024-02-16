using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Application.Contracts;
using Application.Models.MFA;
using Application.Settings;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SignUpResponse = Application.Responses.V1.Users.SignUpResponse;

namespace Infrastructure.Services.Development;

public class DevAuthenticationService : IAuthenticationService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IOptionsMonitor<DevSettings> _devSettings;
    private readonly IDataProtector _dataProtector;
    private readonly IHeaderService _headerService;
    private readonly AwsSettings _awsSettings;
    private readonly IAmazonCognitoIdentityProvider _provider;
    private static readonly byte[] Key = Encoding.ASCII.GetBytes("This is a test key");

    public DevAuthenticationService(IAuthenticationService authenticationService,
        IOptionsMonitor<DevSettings> devSettings, IDataProtectionProvider dataProtector, IHeaderService headerService,
        AwsSettings awsSettings, IAmazonCognitoIdentityProvider provider)
    {
        _authenticationService = authenticationService;
        _devSettings = devSettings;
        _dataProtector = dataProtector.CreateProtector("mfa.login.details");
        _headerService = headerService;
        _awsSettings = awsSettings;
        _provider = provider;
    }

    private string CreateLocalIdToken(string id)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = CreateClaimsIdentity(id),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity CreateClaimsIdentity(string id)
    {
        return new ClaimsIdentity(new[]
        {
            new Claim("sub", id),
            new Claim("email", "test@email.com"),
            new Claim("email_verified", "true"),
            new Claim("name", "John Doe"),
            new Claim("iss", "https://cognito-idp.eu-west-2.amazonaws.com/us-west-1ABCDEFGHI"),
            new Claim("cognito:username", id),
            new Claim("cognito:groups", "Group1"),
            new Claim("phone_number", "+447712345678"),
            new Claim("phone_number_verified", "true")
        });
    }

    public async Task<Response<SignUpResponse>> SignUpAsync(string email, string password, string selectedLocale)
    {
        var response = await _authenticationService.SignUpAsync(email, password, selectedLocale);
        if (_devSettings.CurrentValue.AutoConfirmNewCognitoSignup)
        {
            await _provider.AdminConfirmSignUpAsync(new AdminConfirmSignUpRequest
            {
                Username = email,
                UserPoolId = _awsSettings.CognitoPoolId,
            });
        }

        return response;
    }

    public async Task<Response<string>> RespondToMfaChallengeAsync(string mfaCode, string mfaDetails)
    {
        if (_devSettings.CurrentValue.BypassMfa)
        {
            var mfaLoginDetails = MfaLoginDetails.FromProtectedString(_dataProtector, mfaDetails);
            return Response<string>.CreateSuccessfulContentResponse(
                CreateLocalIdToken(mfaLoginDetails.Username),
                _headerService.GetConversationId());
        }
        else
        {
            return await _authenticationService.RespondToMfaChallengeAsync(mfaCode, mfaDetails);
        }
    }
}
