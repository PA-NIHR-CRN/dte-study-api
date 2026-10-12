namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public class InsertStatement : ISqlTopLevelStatement
{
    public string Table { get; init; }
    public object InsertedValues { get; init; }
    
    public void Accept(ISqlTopLevelStatementVisitor visitor) => visitor.Visit(this);

}