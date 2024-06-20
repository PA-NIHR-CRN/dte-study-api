﻿using System.Linq;
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
            return principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            return principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        }
    }
}