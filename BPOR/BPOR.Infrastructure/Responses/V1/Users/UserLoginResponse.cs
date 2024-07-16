using System.Text.Json.Serialization;

namespace BPOR.Infrastructure.Responses.V1.Users;

public class UserLoginResponse
{
    public string ErrorMessage { get; set; }
    public bool IsNhsLinkedAccount { get; set; }
    public bool IsUserConsented { get; set; }
}

public class TokenResponse
{
    [JsonPropertyName("id_token")] public string IdToken { get; set; }

    [JsonPropertyName("access_token")] public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")] public string RefreshToken { get; set; }

    [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }

    [JsonPropertyName("token_type")] public string TokenType { get; set; }

    [JsonPropertyName("scope")] public string Scope { get; set; }
}
