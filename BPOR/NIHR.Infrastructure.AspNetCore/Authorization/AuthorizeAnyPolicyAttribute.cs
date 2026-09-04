using Microsoft.AspNetCore.Authorization;

namespace CpmsCore.Web.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeAnyPolicyAttribute : AuthorizeAttribute {

    public string[] Policies { get; }

    public AuthorizeAnyPolicyAttribute(params string[] policies)
        => Policies = policies;
}
