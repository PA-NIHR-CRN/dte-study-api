namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public class SqlVariable : ISqlValue
{
    public SqlVariable(string name)
    {
        Name = name;
    }

    public string Name { get; init; }

    public void Accept(ISqlValueVisitor visitor) => visitor.Visit(this);
}