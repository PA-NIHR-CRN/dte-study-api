namespace NIHR.SqlWriter;

public interface ISqlValue
{
    void Accept(ISqlValueVisitor visitor);
}