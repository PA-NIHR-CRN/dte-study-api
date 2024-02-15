using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace Application.Models.MFA;

public class MfaLoginDetails
{
    public string Username { get; set; }
    public string SessionId { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }

    public static string ToProtectedString(IDataProtector dataProtector, AdminInitiateAuthResponse response,
        string password = null)
    {
        var sessionId = response.Session;
        var username = response.ChallengeParameters["USER_ID_FOR_SRP"];
        var phoneNumber = response.ChallengeParameters.ContainsKey("CODE_DELIVERY_DESTINATION")
            ? response.ChallengeParameters["CODE_DELIVERY_DESTINATION"]
            : null;


        return dataProtector.Protect(JsonConvert.SerializeObject(new MfaLoginDetails
        {
            SessionId = sessionId,
            Username = username,
            Password = password,
            PhoneNumber = phoneNumber
        }));
    }

    public static MfaLoginDetails FromProtectedString(IDataProtector dataProtector, string value) =>
        JsonConvert.DeserializeObject<MfaLoginDetails>(dataProtector.Unprotect(value));
}
