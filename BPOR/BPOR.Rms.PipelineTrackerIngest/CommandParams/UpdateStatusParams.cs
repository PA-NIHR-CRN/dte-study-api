using Albatross.CommandLine.Annotations;

namespace BPOR.Rms.PipelineTrackerIngest;

[Verb<UpdateStatusHandler>("update-status", Description = "Updates the status of existing studies from the pipeline tracker")]
public class UpdateStatusParams : PipelineIngestParamsBase;