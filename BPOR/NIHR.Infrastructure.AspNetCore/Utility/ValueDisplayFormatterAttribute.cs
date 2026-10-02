namespace NIHR.Infrastructure.AspNetCore;

public class ValueDisplayFormatterAttribute : Attribute
{
    public Type Type { get; }

    public ValueDisplayFormatterAttribute(Type type)
    {
        Type = type;
    }
}

public class ValueDisplayFormatterAttribute<T> : ValueDisplayFormatterAttribute
    where T : IDisplayStringFormatter
{
    public ValueDisplayFormatterAttribute() : base(typeof(T))
    {
    }
}