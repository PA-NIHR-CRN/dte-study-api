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

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        
        public static bool IsExternalProviderUser(this ClaimsPrincipal principal, string externalProviderName = null)
        {
            var id = principal.FindFirstValue("cognito:username");

            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var split = id.Split("_");

            if (split.Length == 1)
            {
                return false;
            }
            
            return string.IsNullOrWhiteSpace(externalProviderName) || 
                   string.Equals(externalProviderName, split[0], StringComparison.InvariantCultureIgnoreCase);
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

        public static string GetUserFirstname(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.GivenName);
        }
        
        public static string GetUserLastname(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Surname);
        }

        public static bool IsCurrentUser(this ClaimsPrincipal principal, string id)
        {
            var currentUserId = GetParticipantId(principal);

            return string.Equals(currentUserId, id, StringComparison.OrdinalIgnoreCase);
        }
        
        public static string FindFirstValue(this ClaimsPrincipal principal, string claimType)
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