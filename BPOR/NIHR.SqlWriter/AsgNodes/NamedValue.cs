namespace NIHR.SqlWriter;

public record NamedValue(SqlIdentifier Identifier, object? Value);