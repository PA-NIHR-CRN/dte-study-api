namespace NIHR.SqlWriter;

public class UpdateStatement : ISqlTopLevelStatement
{
    public SqlIdentifier Table { get; init; }
    public object UpdatedValues { get; init; }
    public ISqlPredicate WhereClause { get; init; }

    public void Accept(ISqlTopLevelStatementVisitor visitor) => visitor.Visit(this);
}