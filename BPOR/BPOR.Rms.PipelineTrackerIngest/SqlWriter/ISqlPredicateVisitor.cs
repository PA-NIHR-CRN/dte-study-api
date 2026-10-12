namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public interface ISqlPredicateVisitor
{
    void Visit(LogicalBinOpClause predicate);
    void Visit(ComparisonClause predicate);
}