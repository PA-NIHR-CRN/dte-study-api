namespace DYNAMO.STREAM.HANDLER.Entities;

public class Identifier
{
    public int Type { get; set; }
    public string Value { get; set; }

    public Identifier(int type, string value)
    {
        Type = type;
        Value = value;
    }
}
