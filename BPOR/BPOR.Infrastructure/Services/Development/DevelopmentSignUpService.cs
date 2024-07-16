using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using BPOR.Domain.Settings;
using BPOR.Infrastructure.Interfaces;
using Dte.Common.Responses;
using Microsoft.Extensions.Options;
using SignUpResponse = BPOR.Infrastructure.Responses.V1.Users.SignUpResponse;

namespace BPOR.Infrastructure.Services.Development;

public class DevelopmentSignUpService(
    ISignUpService signUpService,
    IOptions<DevelopmentSettings> devSettings,
    IAmazonCognitoIdentityProvider provider,
    AwsSettings awsSettings)
    : ISignUpService
{
    public async Task<Response<SignUpResponse>> SignUpAsync(string email, string password, string selectedLocale,
        CancellationToken cancellationToken)
    {
        var response = await signUpService.SignUpAsync(email, password, selectedLocale, cancellationToken);
        if (devSettings.Value.AutoConfirmNewCognitoSignup)
        {
            await provider.AdminConfirmSignUpAsync(new AdminConfirmSignUpRequest
            {
                Username = email,
                UserPoolId = awsSettings.CognitoPoolId,
            }, cancellationToken);
        }

        return response;
    }
}
