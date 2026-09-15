using System.Linq;
using System.Security.Claims;

namespace NIHR.Infrastructure
{
    public static class AuthenticationExtensions
    {
        public static string GetId (this ClaimsPrincipal principal)
        {
            return principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value?.Trim().ToLowerInvariant();

        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            var givenName = principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            var surname = principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;

            return $"{givenName} {surname}".Trim();
        }
        
        public static int? GetUserId (this ClaimsPrincipal principal)
        {
            var userIdClaim = principal?.FindFirst("UserId")?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim))
                return null;
    
            return int.TryParse(userIdClaim, out var userId) ? userId : null;
        }
    }
}
