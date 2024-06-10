using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BPOR.Rms
{
    public static class ValidationContextExtensions
    {
        public static string GetMemberDisplayName(this ValidationContext validationContext, string memberName) => validationContext.ObjectType.GetProperty(memberName)?.GetCustomAttribute<DisplayAttribute>()?.Name ?? memberName;
    }
}
