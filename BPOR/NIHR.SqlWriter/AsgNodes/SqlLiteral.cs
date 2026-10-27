namespace NIHR.SqlWriter;

public class SqlLiteral : ISqlValue
{
    public object? Value { get; }
    public void Accept(ISqlValueVisitor visitor) => visitor.Visit(this);

    public SqlLiteral(object? value)
    {
        Value = value;
    }
}