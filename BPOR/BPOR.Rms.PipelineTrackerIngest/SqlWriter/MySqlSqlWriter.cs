using System.Text;

namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public class MySqlSqlWriter(TextWriter output) : SqlWriterBase(output)
{
    protected override void WriteLiteral(int value)
    {
        output.Write(value.ToString());
    }

    protected override void WriteLiteral(string value)
    {
        output.Write($"\"{EscapeStringLiteral(value)}\"");
    }

    private static Dictionary<char, string> _escapes = new()
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

    protected override void WriteNullLiteral()
    {
        output.Write("NULL");
    }
    
    public override void Visit(SqlIdentifier identifier)
    {
        output.Write($"`{identifier.Value}`");
    }
}