using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Mocks
{
    public class MockIdentityService: IMockIdentityService
    {
        private static readonly byte[] Key = Encoding.ASCII.GetBytes("This is a test key");

        public string CreateMockIdToken(string id)
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
    }
}
