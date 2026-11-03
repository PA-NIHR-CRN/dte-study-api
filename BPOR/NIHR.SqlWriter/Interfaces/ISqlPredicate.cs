namespace NIHR.SqlWriter;

public interface ISqlPredicate
{
    void Accept(ISqlPredicateVisitor visitor);
}