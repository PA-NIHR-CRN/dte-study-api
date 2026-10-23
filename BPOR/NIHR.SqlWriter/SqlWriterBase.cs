using System.Globalization;

namespace NIHR.SqlWriter;

public abstract class SqlWriterBase(TextWriter output)
    : ISqlWriter, ISqlTopLevelStatementVisitor, ISqlPredicateVisitor, ISqlValueVisitor
{
    protected readonly TokenWriter output = new(output);

    protected virtual int MaxInsertBatchSize => 1000;

    public virtual void WriteComment(string comment)
    {
        if (comment.Contains('\n'))
        {
            output.WriteToken($"/* {comment} */");
        }
        else
        {
            output.WriteToken($"-- {comment}");
            output.EnsureNewLine();
        }
    }

    public virtual void WriteInlineComment(string comment)
    {
        output.WriteToken($" /* {comment} */ ");
    }

    public void WriteRawLine(string line)
    {
        output.WriteRawLine(line);
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
                output.WriteToken(",", suppressWhitespaceBefore: true);
            }

            isFirst = false;
            action(value);
        }
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
        output.WriteToken(value.ToString(CultureInfo.InvariantCulture));
    }

    protected virtual void WriteEndStatement()
    {
        output.WriteToken(";");
        output.EnsureNewLine();
    }

    protected abstract void WriteLiteral(string value);

    protected virtual void WriteNullLiteral() => output.WriteToken("NULL");

    public virtual void Visit(SelectStatement selectStatement)
    {
        output.WriteToken("SELECT");
        WriteIndentedBlock(() =>
        {
            if (selectStatement.Columns.Length == 0)
            {
                output.WriteToken("*");
            }
            else
            {
                WriteCommaSeperated(selectStatement.Columns, column => column.Accept(this));
            }
        });

        output.WriteToken("FROM");
        WriteIndentedBlock(() => selectStatement.Table.Accept(this));

        if (selectStatement.WhereClause is not null)
        {
            output.WriteToken("WHERE");
            WriteIndentedBlock(() => selectStatement.WhereClause.Accept(this));
        }
    }

    public virtual void Visit(UpdateStatement updateStatement)
    {
        output.WriteToken("UPDATE");
        updateStatement.Table.Accept(this);
        output.WriteToken("SET");
        WriteIndentedBlock(() =>
        {
            WriteCommaSeperated(updateStatement.Assignments, i =>
            {
                i.ColumnName.Accept(this);
                output.WriteToken("=");
                i.Value.Accept(this);
            });
        });
        output.WriteToken("WHERE");
        WriteIndentedBlock(() => { updateStatement.WhereClause.Accept(this); });
    }

    public virtual void Visit(InsertStatement insertStatement)
    {
        var batchSize = MaxInsertBatchSize;
        if (batchSize <= 0)
        {
            throw new InvalidOperationException($"{nameof(MaxInsertBatchSize)} must be greater than zero.");
        }

        var batchCount = Math.Max(1, (insertStatement.Values.Length + (long)batchSize - 1) / batchSize);
        for (var batchIndex = 0; batchIndex < batchCount; batchIndex++)
        {
            if (batchIndex > 0)
            {
                WriteEndStatement();
            }

            var startIndex = batchIndex * batchSize;
            var count = Math.Min(batchSize, insertStatement.Values.Length - startIndex);
            WriteInsertBatch(insertStatement, startIndex, count);
        }
    }

    private void WriteIndentedBlock(Action writeBlockContent)
    {
        output.StartIndentedBlock();
        writeBlockContent();
        output.EndIndentedBlock();
    }

    protected virtual void WriteInsertBatch(InsertStatement insertStatement, int startIndex, int count)
    {
        output.WriteToken("INSERT INTO");
        Visit(insertStatement.Table);
        WriteIndentedBlock(() =>
        {
            WriteOpenParenthesis();
            WriteCommaSeperated(insertStatement.Columns, i => i.Accept(this));
            WriteCloseParenthesis();
        });
        output.WriteToken("VALUES");
        WriteIndentedBlock(() =>
        {
            for (var rowIndex = startIndex; rowIndex < startIndex + count; rowIndex++)
            {
                WriteOpenParenthesis();
                WriteCommaSeperated(insertStatement.Values[rowIndex], i => i.Accept(this));
                WriteCloseParenthesis();
                if (rowIndex < startIndex + count - 1)
                {
                    output.WriteToken(",");
                    output.EnsureNewLine();
                }
            }
        });
    }

    public virtual void Visit(SetVariableStatement setVariable)
    {
        output.WriteToken("SET");
        setVariable.Variable.Accept(this);
        output.WriteToken("=");
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
        output.WriteToken(predicateOp switch
        {
            LogicalBinOp.And => "AND",
            LogicalBinOp.Or => "OR",
            _ => throw new NotImplementedException()
        });
    }

    protected virtual void WriteOpenParenthesis()
    {
        output.WriteToken("(", suppressWhitespaceAfter: true);
    }

    protected virtual void WriteCloseParenthesis()
    {
        output.WriteToken(")", suppressWhitespaceBefore: true);
    }

    public void Visit(ComparisonExpr predicate)
    {
        predicate.Lhs.Accept(this);
        WriteComparisonOp(predicate.Op);
        predicate.Rhs.Accept(this);
    }

    protected virtual void WriteComparisonOp(ComparisonOperator op)
    {
        output.WriteToken(op switch
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
        output.WriteToken(value.ToString(CultureInfo.InvariantCulture));
    }

    public abstract void Visit(SqlIdentifier identifier);

    public virtual void Visit(SqlVariable variable)
    {
        output.WriteToken(variable.Name);
    }

    public virtual void Visit(SqlFunction function)
    {
        output.WriteToken(function.Name);
        WriteOpenParenthesis();
        // TODO: Add parameters
        WriteCloseParenthesis();
    }
}