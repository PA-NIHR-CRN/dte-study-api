using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.Extensions.Options;

namespace DeletedUserTool;

public class BporCognito(IOptions<CognitoSettings> settings)
{
    public async IAsyncEnumerable<UserType> GetUserByEmail(string email)
    {
        var config = new AmazonCognitoIdentityProviderConfig()
        {
            Profile = new Profile(settings.Value.Profile),
            RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Value.RegionEndpoint)
        };
        config.Validate();
        using var client = new AmazonCognitoIdentityProviderClient(config);

        string paginationToken = null;
        do
        {
            var request = new ListUsersRequest
            {
                UserPoolId = settings.Value.UserPoolId,
                Limit = 10, // Number of users per page
                PaginationToken = paginationToken,
                Filter = $"email=\"{email}\""
            };

            var response = await client.ListUsersAsync(request);

            // Display users
            foreach (var user in response.Users)
            {
                yield return user;
            }

            paginationToken = response.PaginationToken;
        } while (!string.IsNullOrEmpty(paginationToken));
    }
}