namespace BPOR.Rms.PipelineTrackerIngest.SqlWriter;

public static class MySqlFunctions
{
    public static SqlFunction LastInsertId() => new SqlFunction("LAST_INSERT_ID");
}