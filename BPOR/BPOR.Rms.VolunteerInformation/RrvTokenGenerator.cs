using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BPOR.Rms.VolunteerInformation;

public class RrvTokenGenerator(IOptions<RrvTokenOptions> options) : IRrvTokenGenerator
{
    private const string _participantIdClaimType = "participantId";
    
    public string GenerateToken(long campaignParticipantId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SymmetricKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(_participantIdClaimType, campaignParticipantId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            expires: DateTime.Now.Add(options.Value.Ttl),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool TryValidateToken(string token, out long campaignParticipantId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SymmetricKey));
        var validationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = options.Value.Issuer,
            ValidAudience = options.Value.Audience,
            IssuerSigningKey = securityKey
        };

        try
        {
            var identity = tokenHandler.ValidateToken(token, validationParameters, out _);
            var participantIdString = identity.FindFirst(_participantIdClaimType);
            if (participantIdString != null && long.TryParse(participantIdString.Value, out var parsedParticipantId))
            {
                campaignParticipantId = parsedParticipantId;
                return true;
            }
        }
        catch
        {
            // Intentionally do nothing for now ... but need to handle JWT validation errors.
        }
        
        campaignParticipantId = 0;
        return false;
    }
}