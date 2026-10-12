namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public class LogicalBinOpClause : ISqlPredicate
{
    public void Accept(ISqlPredicateVisitor visitor) => visitor.Visit(this);
    
    public ISqlPredicate Lhs { get; init; }
    public LogicalBinOp Op { get; init; }
    public ISqlPredicate Rhs { get; init; }
}