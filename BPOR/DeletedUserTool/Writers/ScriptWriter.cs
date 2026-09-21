namespace DeletedUserTool.Writers;

public abstract class ScriptWriter : IDisposable
{
    protected IndentedTextWriter TextWriter { get; }

    protected ScriptWriter(TextWriter textWriter)
    {
        TextWriter = new IndentedTextWriter(textWriter);
    }

    public virtual void Dispose()
    {
        TextWriter.Dispose();
    }
    
    public void BeginSection(string comment)
    {
        TextWriter.WriteLine();
        TextWriter.BeginIndent();
        WriteComment(comment);
    }

    public void EndSection()
    {
        TextWriter.EndIndent();
    }

    protected abstract void WriteComment(string comment);
}