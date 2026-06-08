using System.Reflection;

namespace BPOR.Rms.VolunteerInformation.Utility;

public static class MetadataExtensions
{
    public static bool HasAttribute<T>(this MemberInfo info)  where T : Attribute
    {
        return info.GetCustomAttribute(typeof(T)) != null;
    }
}