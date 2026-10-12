using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NIHR.Infrastructure.AspNetCore;

public static class EnumHelper
{
    public static string GetDisplayName<TEnum>(TEnum value) 
        where TEnum : struct, Enum
    {
        var memberInfo = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
        var displayAttribute = memberInfo?.GetCustomAttribute<DisplayAttribute>();
        return displayAttribute?.Name ?? value.ToString() ?? string.Empty;
    }
    
    public static string GetDisplayName<TEnum>(TEnum? value, string nullDisplay = "") 
        where TEnum : struct, Enum
    {
        return value == null ? nullDisplay : GetDisplayName(value.Value);
    }
}