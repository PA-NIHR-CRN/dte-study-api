namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public interface ISqlTopLevelStatement
{
    void Accept(ISqlTopLevelStatementVisitor visitor);
}