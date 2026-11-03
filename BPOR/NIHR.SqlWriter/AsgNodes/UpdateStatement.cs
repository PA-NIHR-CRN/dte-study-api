namespace NIHR.SqlWriter;

public class UpdateStatement : ISqlTopLevelStatement
{
    public SqlIdentifier Table { get; init; }
    
    public UpdateAssignment[] Assignments { get; init; }
    public ISqlPredicate WhereClause { get; init; }

    public void Accept(ISqlTopLevelStatementVisitor visitor) => visitor.Visit(this);
}

public class UpdateAssignment
{
    public SqlIdentifier ColumnName { get; init; }
    public ISqlValue Value { get; init; }
}