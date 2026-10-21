namespace NIHR.SqlWriter;

public interface ISqlPredicateVisitor
{
    void Visit(LogicBinOpExpr predicate);
    void Visit(ComparisonExpr predicate);
}