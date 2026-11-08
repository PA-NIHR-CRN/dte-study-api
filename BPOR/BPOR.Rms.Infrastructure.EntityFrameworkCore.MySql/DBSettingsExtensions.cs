using MySqlConnector;
using BPOR.Rms.Infrastructure.EntityFrameworkCore;

namespace BPOR.Rms.Infrastructure.EntityFrameworkCore.MySql
{
    public static class DBSettingsExtensions
    {
        public static string BuildConnectionString(this DbSettings source, string? databaseName = null)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = source.Host,
                Port = source.Port ?? 3306,
                UserID = source.Username,
                Password = source.Password,
                Database = databaseName ?? source.Database,
                // TODO: Support default DB generated values
                // on timestamp columns and remove these settings.
                AllowZeroDateTime = true,
                ConvertZeroDateTime = true,
            };

            if (source.DefaultCommandTimeout is not null)
            {
                connectionStringBuilder.DefaultCommandTimeout = source.DefaultCommandTimeout.Value;
            }

            return connectionStringBuilder.ConnectionString;
        }
    }
}
