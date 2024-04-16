using MySqlConnector;

namespace NIHR.Infrastructure.EntityFrameworkCore
{
    public static class DBSettingsExtensions
    {
        public static string BuildConnectionString(this DbSettings source)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = source.Host,
                Port = (uint)source.Port,
                UserID = source.Username,
                Password = source.Password,
                Database = source.Database,
            };
            return connectionStringBuilder.ConnectionString;
        }
    }
}
