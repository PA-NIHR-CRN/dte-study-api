namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public class LogicBinOpExpr : ISqlPredicate
{
    public void Accept(ISqlPredicateVisitor visitor) => visitor.Visit(this);
    
    public ISqlPredicate Lhs { get; init; }
    public LogicalBinOp Op { get; init; }
    public ISqlPredicate Rhs { get; init; }
}