using Albatross.CommandLine.Annotations;
using BPOR.Rms.PipelineTrackerIngest.CommandHandlers;

namespace BPOR.Rms.PipelineTrackerIngest.CommandParams;

[Verb<AddMissingStudiesHandler>("add-missing-studies", Description = "Adds a study to RMS for every pipeline sheet row where there isn't an existing matched study in RMS")]
public class AddMissingStudiesParams : PipelineIngestParamsBase;