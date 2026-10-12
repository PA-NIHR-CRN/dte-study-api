namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public interface ISqlValueVisitor
{
    void Visit(SqlLiteral literal);
    void Visit(SqlIdentifier identifier);
    void Visit(SqlVariable variable);
}