namespace NIHR.SqlWriter;

public interface ISqlTopLevelStatementVisitor
{
    void Visit(SelectStatement selectStatement);
    void Visit(UpdateStatement updateStatement);
    void Visit(InsertStatement insertStatement);
    void Visit(SetVariableStatement setVariable);
}