namespace Dynamo.Stream.Handler.Entities;

public class Identifier
{
    public int Type { get; set; }
    public Guid Value { get; set; }

    public Identifier(int type, Guid value)
    {
        Type = type;
        Value = value;
    }
}
