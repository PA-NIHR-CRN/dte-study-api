using Microsoft.AspNetCore.Authorization;

namespace BPOR.Rms.Infrastructure.AspNetCore.Authentication.AccessToken;

public class AccessTokenRequirement(string tokenRole) : IAuthorizationRequirement
{
    public string TokenRole { get; } = tokenRole;
}