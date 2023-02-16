using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace StudyApi.Extensions
{
    public static class AuthExtensions
    {
        private static readonly IEnumerable<string> ScopeClaimTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "http://schemas.microsoft.com/identity/claims/scope",
            "scope"
        };
        
        public static AuthorizationPolicyBuilder RequireScopes(this AuthorizationPolicyBuilder builder, params string[] scopes) {
            return builder.RequireAssertion(context =>
                context.User
                    .Claims
                    .Where(c => ScopeClaimTypes.Contains(c.Type))
                    .SelectMany(c => c.Value.Split(' '))
                    .Any(s => scopes.Contains(s, StringComparer.OrdinalIgnoreCase))
            );
        }
    }
}