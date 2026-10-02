namespace NIHR.Infrastructure.AspNetCore;

public class EnumFormatter<TEnum> : IDisplayStringFormatter
    where TEnum : struct, Enum
{
    public string ToDisplayString(object? value)
    {
        if (value == null)
        {
            return string.Empty;
        }
        
        if (value is not TEnum enumValue)
        {
            throw new InvalidOperationException("value is expected to be of type " + typeof(TEnum).FullName);
        }

        return EnumHelper.GetDisplayName(enumValue);
    }
}