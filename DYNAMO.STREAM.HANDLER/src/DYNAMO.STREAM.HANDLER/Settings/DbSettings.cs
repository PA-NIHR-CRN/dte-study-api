using MySqlConnector;

namespace Dynamo.Stream.Handler.Settings;

public class DbSettings
{
    public static string SectionName => "DbSettings";
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Database { get; set; } = null!;

    public string BuildConnectionString()
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder
        {
            Server = Host,
            Port = (uint)Port,
            UserID = Username,
            Password = Password,
            Database = Database,
        };
        return connectionStringBuilder.ConnectionString;
    }

}
