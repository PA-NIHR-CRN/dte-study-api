namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public interface ISqlPredicateVisitor
{
    void Visit(LogicBinOpExpr predicate);
    void Visit(ComparisonExpr predicate);
}