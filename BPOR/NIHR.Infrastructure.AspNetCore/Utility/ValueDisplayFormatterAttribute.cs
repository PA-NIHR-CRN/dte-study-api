namespace NIHR.Infrastructure.AspNetCore;

public class ValueDisplayFormatterAttribute : Attribute
{
    public Type Type { get; }

    public ValueDisplayFormatterAttribute(Type type)
    {
        Type = type;
    }
}