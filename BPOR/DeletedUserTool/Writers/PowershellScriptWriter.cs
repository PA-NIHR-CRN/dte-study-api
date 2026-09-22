namespace DeletedUserTool.Writers;

public class PowershellScriptWriter : ScriptWriter
{
    public PowershellScriptWriter(TextWriter textWriter) : base(textWriter)
    {
    }

    protected override void WriteComment(string comment)
    {
        TextWriter.WriteLine("# " + comment);
    }
}

