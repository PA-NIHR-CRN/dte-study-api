namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public static class Extensions
{
    public static ISqlWriter Insert(this ISqlWriter writer, SqlIdentifier table, object values)
        => Write(writer, new InsertStatement{Table = table, InsertedValues =  values});
    public static ISqlWriter Update(this ISqlWriter writer, SqlIdentifier table, object values, ISqlPredicate whereClause)
        => Write(writer, new UpdateStatement{Table = table, UpdatedValues = values, WhereClause = whereClause});
    public static ISqlWriter Set(this ISqlWriter writer, SqlVariable variable, ISqlValue value)
        => Write(writer, new SetVariableStatement{Variable = variable, Value = value});

    private static ISqlWriter Write(ISqlWriter writer, ISqlTopLevelStatement statement)
    {
        writer.Write(statement);
        return writer;
    }
}