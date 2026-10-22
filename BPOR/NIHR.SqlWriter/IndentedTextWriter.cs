using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace NIHR.SqlWriter;

public sealed class IndentedTextWriter(TextWriter writer) : TextWriter
{
    private bool _atLineStart = true;
    private int _indentLevel;

    public override Encoding Encoding => writer.Encoding;

    [AllowNull]
    public override string NewLine
    {
        get => writer.NewLine;
        set => writer.NewLine = value ?? Environment.NewLine;
    }

    public int IndentLevel
    {
        get => _indentLevel;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            _indentLevel = value;
        }
    }

    public override void Write(char value)
    {
        if (_atLineStart && value is not ('\r' or '\n'))
        {
            writer.Write(new string(' ', _indentLevel * 4));
            _atLineStart = false;
        }

        writer.Write(value);
        if (value is '\r' or '\n')
        {
            _atLineStart = true;
        }
    }

    public override void Write(string? value)
    {
        if (value is null)
        {
            return;
        }

        foreach (var character in value)
        {
            Write(character);
        }
    }

    public override void Write(char[] buffer, int index, int count)
    {
        ArgumentNullException.ThrowIfNull(buffer);
        foreach (var character in buffer.AsSpan(index, count))
        {
            Write(character);
        }
    }

    public override void Write(ReadOnlySpan<char> buffer)
    {
        foreach (var character in buffer)
        {
            Write(character);
        }
    }

    public override void WriteLine()
    {
        writer.WriteLine();
        _atLineStart = true;
    }

    public void EnsureNewLine()
    {
        if (!_atLineStart)
        {
            WriteLine();
        }
    }
}