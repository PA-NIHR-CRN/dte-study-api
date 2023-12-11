using MySqlConnector;

namespace DYNAMO.STREAM.HANDLER;

public class DbSettings
{
    public static string SectionName => "DbSettings";
    public string Username { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string DbClusterIdentifier { get; set; }
    
    public string BuildConnectionString()
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder
        {
            Server = Host,
            Port = (uint)Port,
            UserID = Username,
            Password = Password,
            Database = DbClusterIdentifier,
        };
        return connectionStringBuilder.ConnectionString;
    }
    
}
