namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public class SqlIdentifier : ISqlValue
{
    public SqlIdentifier(string value)
    {
        Value = value;
    }

    public static implicit operator SqlIdentifier(string value)
    {
        return new SqlIdentifier(value);
    }
    
    public string Value { get; }
    
    public void Accept(ISqlValueVisitor visitor) => visitor.Visit(this);
}