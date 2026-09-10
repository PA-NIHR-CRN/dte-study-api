using Dapper;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace DeletedUserTool;

public class RmsDatabase(IOptions<MySqlSettings> settings)
{
    MySqlConnection CreateConnection()
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder
        {
            Server = settings.Value.Host,
            Port = settings.Value.Port,
            UserID = settings.Value.Username,
            Password = settings.Value.Password,
            Database = settings.Value.Database,
            AllowZeroDateTime = true,
            ConvertZeroDateTime = true,
        };
        MySqlConnection conn = new MySqlConnection(connectionStringBuilder.ConnectionString);
        conn.Open();
        return conn;
    }

    public IGrouping<RmsDeletedParticipant, RmsDeletedParticipantIdentifier>[] GetDeletedParticipants()
    {
        using var conn = CreateConnection();
        var result = conn.Query<RmsDeletedParticipant, RmsDeletedParticipantIdentifier, (RmsDeletedParticipant, RmsDeletedParticipantIdentifier)>(
            File.ReadAllText("Scripts/GetDeletedUsersFromRms.sql"), (a, b) => (a, b), splitOn: "IdentifierId");
        return result.GroupBy(i => i.Item1, i => i.Item2).ToArray();
    }

    public int CountParticipantsByEmail(string email)
    {
        using var conn = CreateConnection();
        var result = conn.ExecuteScalar<int>("COUNT (1) FROM Participants WHERE Email = @Email", new { Email = email });
        return result;
    }
}