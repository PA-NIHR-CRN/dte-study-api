namespace NIHR.SqlWriter;

public class SelectStatement : ISqlTopLevelStatement
{
    public ISqlValue[] Columns { get; init; } = [];
    public required SqlIdentifier Table { get; init; }
    public ISqlPredicate? WhereClause { get; init; }

    public void Accept(ISqlTopLevelStatementVisitor visitor) => visitor.Visit(this);
}