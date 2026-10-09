using System.Reflection;

namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public abstract class SqlWriterBase(TextWriter output) : ISqlWriter
{
    public virtual void WriteComment(string comment)
    {
        if (comment.Contains('\n'))
        {
            output.WriteLine($"/* {comment} */");
        }
        else
        {
            output.WriteLine($"-- {comment}");
        }
    }
    
    public void WriteLine(string line)
    {
        output.WriteLine(line);
    }

    public virtual void Write(InsertStatement statement)
    {
        var values = Flatten(statement.InsertedValues);
        
        output.Write("INSERT INTO ");
        Write(statement.Target);
        output.Write(" (");
        WriteCommaSeperated(values, i => Write(i.Identifier));
        output.Write(") VALUES (");
        WriteCommaSeperated(values, i => Write(i.Value));
        output.WriteLine(");");
    }

    protected virtual void WriteCommaSeperated<T>(IEnumerable<T> values, Action<T> action)
    {
        bool isFirst = true;
        foreach (var value in values)
        {
            if (!isFirst)
            {
                output.Write(", ");
            }
            isFirst = false;
            action(value);
        }
    }

    protected void Write(TypedValue value)
    {
        if (value.Type == typeof(SqlVariable))
        {
            Write((SqlVariable) value.Value);
        }
        else if (value.Value is null)
        {
            WriteNullLiteral();
        }
        else if (value.Type == typeof(string))
        {
            WriteLiteral((string)value.Value);
        }
        else if (value.Type == typeof(bool) || value.Type == typeof(bool?))
        {
            WriteLiteral((bool)value.Value);
        }
        else if (value.Type == typeof(DateTime) || value.Type == typeof(DateTime?))
        {
            WriteLiteral((DateTime)value.Value);
        }
        else if (value.Type == typeof(int) || value.Type == typeof(int?))
        {
            WriteLiteral((int)value.Value);
        }
    }

    protected abstract void WriteLiteral(DateTime value);

    protected abstract void WriteLiteral(bool value);

    protected abstract void WriteLiteral(int value);

    protected abstract void WriteLiteral(string value);

    protected abstract void WriteNullLiteral();

    protected abstract void Write(SqlVariable value);
    
    protected abstract void Write(SqlIdentifier identifier);

    private IEnumerable<NamedValue> Flatten(object value)
    {
        foreach (var item in value.GetType()
                     .GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public))
        {
            yield return new NamedValue(item.Name, new (item.PropertyType, item.GetValue(value)));
        }
    }
}

public interface ISqlWriter
{
    void WriteComment(string comment);
    void WriteLine(string line);
    void Write(InsertStatement statement);
}