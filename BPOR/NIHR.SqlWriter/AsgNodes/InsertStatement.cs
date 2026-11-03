namespace NIHR.SqlWriter;

public class InsertStatement : ISqlTopLevelStatement
{
    public SqlIdentifier Table { get; init; }
    public SqlIdentifier[]  Columns { get; init; }
    public ISqlValue[][] Values { get; init; }
    
    public void Accept(ISqlTopLevelStatementVisitor visitor) => visitor.Visit(this);
}