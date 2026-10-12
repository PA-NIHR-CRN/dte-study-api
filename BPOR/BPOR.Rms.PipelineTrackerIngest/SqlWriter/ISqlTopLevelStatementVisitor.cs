namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public interface ISqlTopLevelStatementVisitor
{
    void Visit(UpdateStatement updateStatement);
    void Visit(InsertStatement insertStatement);
}