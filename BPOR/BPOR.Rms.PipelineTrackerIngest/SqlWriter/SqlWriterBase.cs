using System.Globalization;
using System.Reflection;

namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public abstract class SqlWriterBase(TextWriter output) : ISqlWriter, ISqlTopLevelStatementVisitor, ISqlPredicateVisitor, ISqlValueVisitor
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
    
    public void WriteRawLine(string line)
    {
        output.WriteLine(line);
    }

    public void Write(ISqlTopLevelStatement statement)
    {
        statement.Accept(this);
        WriteEndStatement();
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

    protected void WriteReflectedValue(object? value)
    {
        if (value is not ISqlValue sqlValue)
        {
            sqlValue = new SqlLiteral(value);
        }
        sqlValue.Accept(this);
    }

    protected virtual void WriteLiteral(DateTime value)
    {
        WriteLiteral(value.ToString("yyyy-MM-ddTHH:mm:ss"));
    }

    protected virtual void WriteLiteral(bool value)
    {
        WriteLiteral(value ? 1 : 0);
    }

    protected virtual void WriteLiteral(int value)
    {
        output.Write(value.ToString(CultureInfo.InvariantCulture));
    }

    protected virtual void WriteEndStatement()
    {
        output.WriteLine(";");
    }

    protected abstract void WriteLiteral(string value);

    protected virtual void WriteNullLiteral() => output.Write("NULL");

    private IEnumerable<NamedValue> Flatten(object value)
    {
        foreach (var item in value.GetType()
                     .GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public))
        {
            yield return new NamedValue(item.Name, new (item.PropertyType, item.GetValue(value)));
        }
    }

    public virtual void Visit(UpdateStatement updateStatement)
    {
        var flattenedValues = Flatten(updateStatement.UpdatedValues);
        
        output.Write("UPDATE ");
        updateStatement.Table.Accept(this);
        output.Write(" SET ");
        WriteCommaSeperated(flattenedValues, i =>
        {
            i.Identifier.Accept(this);
            output.Write(" = ");
            WriteReflectedValue(i.Value.Value);
        });
        output.Write(" WHERE ");
        updateStatement.WhereClause.Accept(this);
    }

    public virtual void Visit(InsertStatement insertStatement)
    {
        var values = Flatten(insertStatement.InsertedValues);
        
        output.Write("INSERT INTO ");
        Visit(insertStatement.Table);
        WriteOpenParanthesis();
        WriteCommaSeperated(values, i => Visit(i.Identifier));
        WriteCloseParanthesis();
        output.Write(" VALUES ");
        WriteOpenParanthesis();
        WriteCommaSeperated(values, i => WriteReflectedValue(i.Value.Value));
        WriteCloseParanthesis();
    }

    public void Visit(LogicalBinOpClause predicate)
    {
        WriteOpenParanthesis();
        predicate.Lhs.Accept(this);
        WriteLogicalBinOp(predicate.Op);
        predicate.Rhs.Accept(this);
        WriteCloseParanthesis();
    }

    protected virtual void WriteLogicalBinOp(LogicalBinOp predicateOp)
    {
        output.Write(predicateOp switch
        {
            LogicalBinOp.And => "AND",
            LogicalBinOp.Or => "OR",
            _ => throw new NotImplementedException()
        });
    }

    private void WriteOpenParanthesis()
    {
        output.Write("(");
    }
    
    private void WriteCloseParanthesis()
    {
        output.Write(")");
    }

    public void Visit(ComparisonClause predicate)
    {
        predicate.Lhs.Accept(this);
        WriteComparisonOp(predicate.Op);
        predicate.Rhs.Accept(this);
    }

    protected virtual void WriteComparisonOp(ComparisonOperator op)
    {
        output.Write(" ");
        output.Write(op switch
        {
            ComparisonOperator.Equals => "=",
            ComparisonOperator.NotEquals => "<>",
            ComparisonOperator.GreaterThan => ">",
            ComparisonOperator.GreaterThanOrEquals => ">=",
            ComparisonOperator.LessThan => "<",
            ComparisonOperator.LessThanOrEquals => "<=",
            ComparisonOperator.Like => "LIKE",
            ComparisonOperator.In => "IN",
            _ => throw new NotImplementedException()
        });
        output.Write(" ");
    }

    public void Visit(SqlLiteral literal)
    {
        if (literal.Value is null)
        {
            WriteNullLiteral();
        }
        else if (literal.Value is Enum enumValue)
        {
            WriteLiteral(Convert.ToInt32(enumValue));
        }
        else if (literal.Value is string stringValue)
        {
            WriteLiteral(stringValue);
        }
        else if (literal.Value is bool boolValue)
        {
            WriteLiteral(boolValue);
        }
        else if (literal.Value is DateTime dateTimeValue)
        {
            WriteLiteral(dateTimeValue);
        }
        else if (literal.Value is int intValue)
        {
            WriteLiteral(intValue);
        }
        else
        { 
            throw new InvalidOperationException($"Unsupported literal type {literal.Value.GetType().Name}");
        }
    }

    public abstract void Visit(SqlIdentifier identifier);
    public virtual void Visit(SqlVariable variable)
    {
        output.Write(variable.Name);
    }
}