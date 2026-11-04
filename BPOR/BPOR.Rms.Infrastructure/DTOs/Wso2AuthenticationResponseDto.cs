using System.Text.Json.Serialization;

namespace BPOR.Rms.Infrastructure.DTOs
{
    public class Wso2AuthenticationResponseDto
    {
        [JsonPropertyName("access_token")] public string AccessToken { get; set; } = string.Empty;
        [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }
    }
}
