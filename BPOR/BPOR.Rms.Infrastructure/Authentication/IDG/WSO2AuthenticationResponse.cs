using System.Text.Json.Serialization;

namespace BPOR.Rms.Infrastructure.Authentication.IDG;

public class WSO2AuthenticationResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}