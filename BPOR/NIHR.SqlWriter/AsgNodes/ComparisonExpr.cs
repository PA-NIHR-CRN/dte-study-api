namespace NIHR.SqlWriter;

public class ComparisonExpr : ISqlPredicate
{
    public void Accept(ISqlPredicateVisitor visitor) => visitor.Visit(this);
    
    public ISqlValue Lhs { get; init; }
    public ComparisonOperator Op { get; init; }
    public ISqlValue Rhs { get; init; }
}