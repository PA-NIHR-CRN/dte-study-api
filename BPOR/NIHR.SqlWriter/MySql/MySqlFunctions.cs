namespace NIHR.SqlWriter;

public static class MySqlFunctions
{
    public static SqlFunction LastInsertId() => new("LAST_INSERT_ID");
}