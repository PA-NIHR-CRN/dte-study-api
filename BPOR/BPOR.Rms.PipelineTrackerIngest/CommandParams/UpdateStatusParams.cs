using Albatross.CommandLine.Annotations;
using BPOR.Rms.PipelineTrackerIngest.CommandHandlers;

namespace BPOR.Rms.PipelineTrackerIngest.CommandParams;

[Verb<UpdateStatusHandler>("update-status", Description = "Updates the status of existing studies from the pipeline tracker")]
public class UpdateStatusParams : PipelineIngestParamsBase;