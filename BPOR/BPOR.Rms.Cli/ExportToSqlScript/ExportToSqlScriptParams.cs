using Albatross.CommandLine.Annotations;

namespace BPOR.Rms.Cli.ExportToSqlScript;

[Verb<ExportToSqlScriptHandler>("export-to-sql", Description = "Exports the complete set of studies to a SQL file (To be made generic)")]
public class ExportToSqlScriptParams
{
    [Option(Description = "The path to the output the SQL files")]
    public string? Destination { get; init; }
    
    [Option("user-id", Description = "The ID of the user to use for created/updated stamps")]
    public required int UserId { get; set; }
}