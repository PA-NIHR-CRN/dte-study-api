namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public interface ISqlPredicate
{
    void Accept(ISqlPredicateVisitor visitor);
}