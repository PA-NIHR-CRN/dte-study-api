using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace BPOR.Rms.VolunteerInformation;

public class RrvTokenGenerator(IDataProtectionProvider dataProtectionProvider, IOptions<RrvTokenOptions> options) : IRrvTokenGenerator
{
    private const string DataProtectionPurpose = "rrvToken";

    private class Token
    {
        [JsonPropertyName("exp")]
        public DateTime ExpiresUtc { get; set; }
        [JsonPropertyName("cpi")]
        public long CampaignParticipantId { get; set; }
    }
    
    public string GenerateToken(long campaignParticipantId)
    {
        var protector = dataProtectionProvider.CreateProtector(DataProtectionPurpose);

        var plaintextBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Token
        {
            CampaignParticipantId = campaignParticipantId,
            ExpiresUtc = DateTime.UtcNow.Add(options.Value.Ttl)
        }));

        var cipheredBytes = protector.Protect(plaintextBytes);
        
        return Convert.ToHexString(cipheredBytes);
    }

    public bool TryValidateToken(string token, out long campaignParticipantId)
    {
        var protector = dataProtectionProvider.CreateProtector(DataProtectionPurpose);

        try
        {
            var cipheredBytes = Convert.FromHexString(token);
            var plaintextBytes = protector.Unprotect(cipheredBytes);
            var plaintext = Encoding.UTF8.GetString(plaintextBytes);
            var plainToken = JsonSerializer.Deserialize<Token>(plaintext);

            if (plainToken == null || plainToken.ExpiresUtc < DateTime.UtcNow)
            {
                campaignParticipantId = 0;
                return false;
            }
            
            campaignParticipantId = plainToken.CampaignParticipantId;
            return true;
        }
        catch
        {
            campaignParticipantId = 0;
            return false;
        }
    }
}