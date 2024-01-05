using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Linq;
using System.Security.Claims;

namespace Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
        }
        
        public static string GetParticipantId(this ClaimsPrincipal principal)
        {
            var id = principal.FindFirstValue("cognito:username");
       
            if (string.IsNullOrWhiteSpace(id))
            {
                var nhsId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

                return !string.IsNullOrWhiteSpace(nhsId) ? nhsId : null;
            }

            var split = id.Split("_");
            
            return split.LastOrDefault();
        }

        private static string FindFirstValue(this ClaimsPrincipal principal, string claimType)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst(claimType);
            return claim?.Value;
        }
    }
}
