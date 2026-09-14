using Microsoft.AspNetCore.Authorization;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public class AccessTokenRequirement(string tokenRole) : IAuthorizationRequirement
{
    public string TokenRole { get; } = tokenRole;
}