using Dapper;
using DeletedUserTool.Models.Rms;
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

    public string[] GetDeletedParticipantEmails()
    {
        using var conn = CreateConnection();
        var result = conn.Query<string>(File.ReadAllText("Scripts/GetDeletedUsersFromRms.sql"));
        return result.ToArray();
    }

    public int CountParticipantsByEmail(string email)
    {
        using var conn = CreateConnection();
        var result = conn.ExecuteScalar<int>("COUNT (1) FROM Participants WHERE Email = @Email", new { Email = email });
        return result;
    }

    public Participant[] GetParticipantsByEmail(string emailAddress)
    {
        using var conn = CreateConnection();
        var result = conn.Query<Participant>("SELECT Id, Email FROM dte.Participants WHERE Email = @Email", new { Email = emailAddress });
        return result.ToArray();
    }
    
    public ParticipantIdentifier[] GetParticipantsIdentifiers(int partificpantId)
    {
        using var conn = CreateConnection();
        var result = conn.Query<ParticipantIdentifier>("SELECT Id, Value, IdentifierTypeId FROM dte.ParticipantIdentifiers WHERE ParticipantId = @ParticipantId", new { ParticipantId = partificpantId });
        return result.ToArray();
    }
}