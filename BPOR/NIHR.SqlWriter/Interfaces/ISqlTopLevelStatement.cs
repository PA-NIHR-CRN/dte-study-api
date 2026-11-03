namespace NIHR.SqlWriter;

public interface ISqlTopLevelStatement
{
    void Accept(ISqlTopLevelStatementVisitor visitor);
}