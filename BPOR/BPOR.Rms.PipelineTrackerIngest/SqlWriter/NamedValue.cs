namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public record NamedValue(SqlIdentifier Identifier, TypedValue Value);