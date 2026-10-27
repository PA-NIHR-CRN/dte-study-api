namespace NIHR.SqlWriter;

public interface ISqlValueVisitor
{
    void Visit(SqlLiteral literal);
    void Visit(SqlIdentifier identifier);
    void Visit(SqlVariable variable);
    void Visit(SqlFunction function);
}