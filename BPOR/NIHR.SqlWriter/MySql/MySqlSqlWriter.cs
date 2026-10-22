using System.Text;

namespace NIHR.SqlWriter;

public class MySqlSqlWriter(TextWriter writer) : SqlWriterBase(writer)
{
    protected override void WriteLiteral(string value)
    {
        output.Write($"\"{EscapeStringLiteral(value)}\"");
    }

    private static readonly Dictionary<char, string> _escapes = new()
    {
        ['\''] = "\\\'",
        ['\"'] = "\\\"",
        ['\n'] = "\\n",
        ['\r'] = "\\r",
        ['\t'] = "\\t",
        ['\\'] = "\\"
    };

    private string EscapeStringLiteral(string value)
    {
        StringBuilder result = new StringBuilder(value.Length);
        foreach (char c in value)
        {
            if (_escapes.TryGetValue(c, out var escaped))
            {
                result.Append(escaped);
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    public override void Visit(SqlIdentifier identifier)
    {
        output.Write($"`{identifier.Value}`");
    }
}