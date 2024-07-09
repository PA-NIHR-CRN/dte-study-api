using BPOR.Domain.Entities;

namespace BPOR.Rms;

public static class UserExtensions
{
    public static bool HasRole(this User? user, string role) => user?.UserRoles.Any(r => string.Equals(r.Role.Code, role, StringComparison.InvariantCultureIgnoreCase)) ?? false;

    public static bool HasRole(this User? user, Domain.Enums.UserRole role) => user?.UserRoles.Any(r => r.RoleId == (int)role) ?? false;
}
