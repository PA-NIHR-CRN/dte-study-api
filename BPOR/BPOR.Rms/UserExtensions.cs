using BPOR.Domain.Entities;
using BPOR.Rms.Startup;

namespace BPOR.Rms;

public static class UserExtensions
{
    public static bool HasRole(this User? user, string role) => user?.UserRoles.Any(r => string.Equals(r.Role.Code, role, StringComparison.InvariantCultureIgnoreCase)) ?? false;

    public static bool HasRole(this User? user, Domain.Enums.UserRole role) => user?.UserRoles.Any(r => r.RoleId == (int)role) ?? false;
    
    public static bool IsResearcher(this User? user) => HasRole(user, Domain.Enums.UserRole.Researcher);
    
    public static bool IsAdmin(this User? user) => HasRole(user, Domain.Enums.UserRole.Admin);
    
    public static bool IsResearcher(this ICurrentUserProvider<User> userProvider) => HasRole(userProvider.User, Domain.Enums.UserRole.Researcher);
    
    public static bool IsAdmin(this ICurrentUserProvider<User> userProvider) => HasRole(userProvider.User, Domain.Enums.UserRole.Admin);
}
