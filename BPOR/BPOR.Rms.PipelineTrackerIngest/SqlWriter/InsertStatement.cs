namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public class InsertStatement
{
    public string Target { get; init; }
    public object InsertedValues { get; init; }
}