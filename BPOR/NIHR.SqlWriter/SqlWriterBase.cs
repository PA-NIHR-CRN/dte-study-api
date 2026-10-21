using System.Globalization;
using System.Reflection;

namespace NIHR.SqlWriter;

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
    
    public virtual void WriteInlineComment(string comment)
    {
        output.Write($" /* {comment} */ ");
    }
    
    public void WriteRawLine(string line)
    {
        output.WriteLine(line);
    }

    public virtual void Write(ISqlTopLevelStatement statement)
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

    private NamedValue[] ExtractNamedValues(object value)
    {
        var propertyInfos = value.GetType()
            .GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
        var result = new NamedValue[propertyInfos.Length];
        for (var index = 0; index < propertyInfos.Length; index++)
        {
            var item = propertyInfos[index];
            result[index] = new NamedValue(item.Name, item.GetValue(value));
        }

        return result;
    }

    public virtual void Visit(UpdateStatement updateStatement)
    {
        var flattenedValues = ExtractNamedValues(updateStatement.UpdatedValues);
        
        output.Write("UPDATE ");
        updateStatement.Table.Accept(this);
        output.Write(" SET ");
        WriteCommaSeperated(flattenedValues, i =>
        {
            i.Identifier.Accept(this);
            output.Write(" = ");
            WriteReflectedValue(i.Value);
        });
        output.Write(" WHERE ");
        updateStatement.WhereClause.Accept(this);
    }

    public virtual void Visit(InsertStatement insertStatement)
    {
        var values = ExtractNamedValues(insertStatement.InsertedValues);
        
        output.Write("INSERT INTO ");
        Visit(insertStatement.Table);
        WriteOpenParenthesis();
        WriteCommaSeperated(values, i => Visit(i.Identifier));
        WriteCloseParenthesis();
        output.Write(" VALUES ");
        WriteOpenParenthesis();
        WriteCommaSeperated(values, i => WriteReflectedValue(i.Value));
        WriteCloseParenthesis();
    }

    public virtual void Visit(SetVariableStatement setVariable)
    {
        output.Write("SET ");
        setVariable.Variable.Accept(this);
        output.Write(" = ");
        setVariable.Value.Accept(this);
    }

    public virtual void Visit(LogicBinOpExpr predicate)
    {
        WriteOpenParenthesis();
        predicate.Lhs.Accept(this);
        WriteLogicalBinOp(predicate.Op);
        predicate.Rhs.Accept(this);
        WriteCloseParenthesis();
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

    protected virtual void WriteOpenParenthesis()
    {
        output.Write("(");
    }
    
    protected virtual void WriteCloseParenthesis()
    {
        output.Write(")");
    }

    public void Visit(ComparisonExpr predicate)
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

    public virtual void Visit(SqlLiteral literal)
    {
        if (literal.Value is null)
        {
            WriteNullLiteral();
        }
        else if (literal.Value is Enum enumValue)
        {
            WriteLiteral(Convert.ToInt32(enumValue));
            WriteInlineComment(enumValue.ToString());
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
        else if (literal.Value is long longValue)
        {
            WriteLiteral(longValue);
        }
        else if (literal.Value is Guid guidValue)
        {
            WriteLiteral(guidValue);
        }
        else
        { 
            throw new InvalidOperationException($"Unsupported literal type {literal.Value.GetType().Name}");
        }
    }

    private void WriteLiteral(Guid value)
    {
        WriteLiteral(value.ToString("N"));
    }

    private void WriteLiteral(long value)
    {
        output.Write(value.ToString(CultureInfo.InvariantCulture));
    }

    public abstract void Visit(SqlIdentifier identifier);
    public virtual void Visit(SqlVariable variable)
    {
        output.Write(variable.Name);
    }

    public virtual void Visit(SqlFunction function)
    {
        output.Write(function.Name);
        WriteOpenParenthesis();
        // TODO: Add parameters
        WriteCloseParenthesis();
    }
}