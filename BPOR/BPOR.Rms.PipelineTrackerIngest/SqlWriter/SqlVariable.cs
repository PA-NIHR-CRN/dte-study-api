namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public class SqlVariable
{
    public SqlVariable(string name)
    {
        Name = name;
    }

    public string Name { get; init; }
}