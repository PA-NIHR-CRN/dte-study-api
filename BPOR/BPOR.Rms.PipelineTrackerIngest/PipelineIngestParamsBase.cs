using Albatross.CommandLine.Annotations;

namespace BPOR.Rms.PipelineTrackerIngest;

public abstract class PipelineIngestParamsBase 
{
    [Argument(Description = "The path to the source CSV file")]
    public required string Source { get; init; }
}