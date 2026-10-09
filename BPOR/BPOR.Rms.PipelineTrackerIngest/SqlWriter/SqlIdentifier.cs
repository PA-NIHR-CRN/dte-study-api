namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public class SqlIdentifier
{
    public SqlIdentifier()
    {
    }

    public static implicit operator SqlIdentifier(string value)
    {
        return new SqlIdentifier { Value = value };
    }
    
    public required string Value { get; init; }
}