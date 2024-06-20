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
                Port = source.Port ?? 3306,
                UserID = source.Username,
                Password = source.Password,
                Database = source.Database,
                // TODO: Support default DB generated values
                // on timestamp columns and remove these settings.
                AllowZeroDateTime = true,
                ConvertZeroDateTime = true,
            };
            return connectionStringBuilder.ConnectionString;
        }
    }
}
