namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public interface ISqlValue
{
    void Accept(ISqlValueVisitor visitor);
}