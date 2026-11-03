using System.Reflection;

namespace NIHR.SqlWriter;

public static class Extensions
{
    public static ISqlWriter Insert<T>(this ISqlWriter writer, SqlIdentifier table, T row)
        => InsertBatch<T>(writer, table, [row]);
                
    public static ISqlWriter InsertBatch<T>(this ISqlWriter writer, SqlIdentifier table, IEnumerable<T> rows)
    {
        var propertyInfos = typeof(T).GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
        var columns = propertyInfos.Select(propertyInfo => new SqlIdentifier(propertyInfo.Name)).ToArray();
        var values = rows.Select(row => propertyInfos.Select(pi => GetSqlValue(pi.GetValue(row))).ToArray()).ToArray();
            
        return Write(writer, new InsertStatement { Table = table, Columns = columns, Values = values });
    }

    private static ISqlValue GetSqlValue(object? value) => value as ISqlValue ?? new SqlLiteral(value);

    public static ISqlWriter Update<T>(this ISqlWriter writer, SqlIdentifier table, T values, ISqlPredicate whereClause)
    {
        var propertyInfos = typeof(T).GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
        var assignments = propertyInfos.Select(propertyInfo => new UpdateAssignment
        {
            ColumnName = propertyInfo.Name,
            Value = GetSqlValue(propertyInfo.GetValue(values))
        }).ToArray();

        return Write(writer, new UpdateStatement { Table = table, Assignments = assignments, WhereClause = whereClause });
    }

    public static ISqlWriter Set(this ISqlWriter writer, SqlVariable variable, ISqlValue value)
        => Write(writer, new SetVariableStatement{Variable = variable, Value = value});

    private static ISqlWriter Write(ISqlWriter writer, ISqlTopLevelStatement statement)
    {
        writer.Write(statement);
        return writer;
    }
}