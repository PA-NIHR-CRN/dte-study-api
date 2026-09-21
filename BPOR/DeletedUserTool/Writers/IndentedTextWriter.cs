namespace DeletedUserTool.Writers;

public class IndentedTextWriter(TextWriter textWriter, int indentLength = 4) : IDisposable, IAsyncDisposable
{
    int _indentCount = 0;
    
    public void Dispose()
    {
        textWriter.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await textWriter.DisposeAsync();
    }
    
    string IndentString(string text, bool stayOnCurrentLine)
    {
        var result = _isAtStartOfNewLine ? new string(' ', _indentCount * indentLength) + text : text;
        _isAtStartOfNewLine = !stayOnCurrentLine;
        return result;
    }
    private bool _isAtStartOfNewLine = true;
    
    public void WriteLine()
    {
        _isAtStartOfNewLine = true;
        textWriter.WriteLine();
    }

    public void WriteLine(string line) => textWriter.WriteLine(IndentString(line, false));
    public void Write(string text) => textWriter.Write(IndentString(text, true));

    public void BeginIndent()
    {
        _indentCount++;
    }
    
    public void EndIndent()
    {
        if (_indentCount > 0)
        {
            _indentCount--;
        }
    }
}