namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public class ComparisonClause : ISqlPredicate
{
    public void Accept(ISqlPredicateVisitor visitor) => visitor.Visit(this);
    
    public ISqlValue Lhs { get; init; }
    public ComparisonOperator Op { get; init; }
    public ISqlValue Rhs { get; init; }
}