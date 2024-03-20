using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BPOR.Domain.Settings;
using BPOR.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BPOR.Infrastructure.Services;

public class NhsLoginClientAssertionJwtProvider(
    IPrivateKeyProvider privateKeyProvider,
    IOptions<NhsLoginSettings> nhsLoginSettings) : IClientAssertionJwtProvider
{
    public async Task<string> CreateClientAssertionJwtAsync(CancellationToken cancellationToken)
    {
        var signingCredentials = await GetSigningCredentialsAsync(cancellationToken);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nhsLoginSettings.Value.ClientId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }),
            Audience = $"{nhsLoginSettings.Value.BaseUrl}{nhsLoginSettings.Value.TokenEndpoint}",
            Issuer = nhsLoginSettings.Value.ClientId,
            // Nhs Login accepts a maximum expiry of 60 minutes, allow 5 minutes for clock drift
            Expires = DateTime.UtcNow.AddMinutes(60).AddMinutes(-5),
            SigningCredentials = signingCredentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }

    public async Task<string> CreateSsoJwtAsync(string jti, CancellationToken cancellationToken)
    {
        var signingCredentials = await GetSigningCredentialsAsync(cancellationToken);

        var now = DateTime.UtcNow;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = nhsLoginSettings.Value.ClientId,
            IssuedAt = now,
            Expires = now.AddSeconds(60),
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }),
            SigningCredentials = signingCredentials,
        };
        if (tokenDescriptor.Claims == null)
        {
            tokenDescriptor.Claims = new Dictionary<string, object>();
        }

        tokenDescriptor.Claims.Add("code", jti);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }

    private async Task<SigningCredentials> GetSigningCredentialsAsync(CancellationToken cancellationToken)
    {
        var rsaKey = await privateKeyProvider.GetPrivateKeyAsync(cancellationToken);
        var rsaParams = rsaKey.ExportParameters(true);

        var key = new RsaSecurityKey(RSA.Create(rsaParams));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha512);
        return signingCredentials;
    }
}
