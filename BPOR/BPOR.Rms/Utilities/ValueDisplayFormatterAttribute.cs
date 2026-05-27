namespace BPOR.Rms.Utilities;

public class ValueDisplayFormatterAttribute : Attribute
{
    public Type Type { get; }

    public ValueDisplayFormatterAttribute(Type type)
    {
        Type = type;
    }
}