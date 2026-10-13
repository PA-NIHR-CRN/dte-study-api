namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public class SqlFunction : ISqlValue
{
    public SqlFunction(string name)
    {
        Name = name;
    }

    public SqlFunction()
    {
    }

    public string Name { get; set; }
    
    // TODO: Add parameters
    
    public void Accept(ISqlValueVisitor visitor)
    {
        visitor.Visit(this);
    }
}