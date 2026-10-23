namespace NIHR.SqlWriter;

public sealed class TokenWriter(TextWriter writer)
{
    private const int _indentWidth = 4;
    private bool _atLineStart = true;
    private int _indentLevel;
    private bool _suppressWhitespaceAfter;
    private int _pendingIndentOffset;

    public void StartIndentedBlock()
    {
        _indentLevel++;
        EnsureNewLine();
    }
    
    public void EndIndentedBlock()
    {
        if (_indentLevel + _pendingIndentOffset <= 0)
        {
            throw new InvalidOperationException();
        }
        _pendingIndentOffset--;
        EnsureNewLine();
    }
    
    private void Write(char value)
    {
        if (_atLineStart && value is not ('\r' or '\n'))
        {
            writer.Write(new string(' ', _indentLevel * _indentWidth));
            _atLineStart = false;
        }

        writer.Write(value);
        if (value is '\r' or '\n')
        {
            _atLineStart = true;
        }
    }

    public void WriteToken(string token, bool suppressWhitespaceBefore = false, bool suppressWhitespaceAfter = false)
    {
        if (!_atLineStart && !suppressWhitespaceBefore && !_suppressWhitespaceAfter)
        {
            writer.Write(' ');    
        }

        Write(token);
        _suppressWhitespaceAfter = suppressWhitespaceAfter;
    }

    private void Write(string value)
    {
        foreach (var character in value)
        {
            Write(character);
        }
    }

    public void EnsureNewLine()
    {
        if (!_atLineStart)
        {
            writer.WriteLine();
            _atLineStart = true;
        }
    }

    public void WriteRawLine(string line)
    {
        EnsureNewLine();
        Write(line);
        EnsureNewLine();
    }
}