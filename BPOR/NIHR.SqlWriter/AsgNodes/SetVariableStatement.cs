namespace NIHR.SqlWriter;

public class SetVariableStatement : ISqlTopLevelStatement
{
    public SqlVariable Variable { get; init; }
    public ISqlValue Value { get; init; }
    
    public void Accept(ISqlTopLevelStatementVisitor visitor)
    {
        visitor.Visit(this);
    }
}