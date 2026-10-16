using Albatross.CommandLine.Annotations;
using BPOR.Rms.PipelineTrackerIngest.CommandHandlers;

namespace BPOR.Rms.PipelineTrackerIngest.CommandParams;

[Verb<CreateIngestScriptsHandler>("create-scripts", Description = "Updates the status of existing studies from the pipeline tracker")]
public class CreateIngestScriptsParams
{
    [Argument(Description = "The path to the source CSV file")]
    public required string Source { get; init; }
    
    [Option(Description = "The path to the output the SQL files")]
    public string? Destination { get; init; }

    [Option("user-id", Description = "The ID of the user to use for created/updated stamps")]
    public required int UserId { get; set; }
}