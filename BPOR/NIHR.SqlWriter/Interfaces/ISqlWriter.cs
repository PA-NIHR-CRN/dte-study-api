namespace NIHR.SqlWriter;

public interface ISqlWriter
{
    void WriteComment(string comment);
    void WriteRawLine(string line);
    void Write(ISqlTopLevelStatement statement);
}