using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NIHR.CRN.CPMS.Database.Extensions
{
    public static class MigrationBuilderExtensions
    {
        public enum MigrationDirection
        {
            Up,
            Down
        }


        /// <summary>
        /// Builds an Microsoft.EntityFrameworkCore.Migrations.Operations.SqlOperation to
        /// include raw SQL from the file identified by <paramref name="scriptIdentifier"/>.
        /// 
        /// Save the SQL content in 'Scripts/MigrationsSQL/{scriptIdentifier}/{scriptIdentifier}[.{stepIdentifier}].{direction}.sql'.
        /// </summary>
        /// <param name="scriptIdentifier">Identifer for the SQL file located in 'Scripts/MigrationsSQL/{scriptIdentifier}/{scriptIdentifier}[.{stepIdentifier}].{direction}.sql'.</param>
        /// <param name="direction">Direction of the migration operation.</param>
        /// <param name="stepIdentifier">Optional step identifer.</param>
        /// <returns>A builder to allow annotations to be added to the operation.</returns>
        public static OperationBuilder<SqlOperation> SqlFromFile(this MigrationBuilder migrationBuilder, string scriptIdentifier, MigrationDirection direction, string stepIdentifier = null)
        {
            return migrationBuilder.Sql(GetSqlFromFile(scriptIdentifier, direction, stepIdentifier));
        }

        /// <summary>
        /// Builds an Microsoft.EntityFrameworkCore.Migrations.Operations.SqlOperation to
        /// include SQL from the file identified by <paramref name="scriptIdentifier"/>.
        /// SQL is included in the migration inside an EXECUTE() statement.
        /// 
        /// Use this method if the included SQL needs to be run in its own batch.
        /// 
        /// Save the SQL content in 'Scripts/MigrationsSQL/{scriptIdentifier}/{scriptIdentifier}[.{stepIdentifier}].{direction}.sql'.
        /// </summary>
        /// <param name="scriptIdentifier">Identifer for the SQL file located in 'Scripts/MigrationsSQL/{scriptIdentifier}/{scriptIdentifier}[.{stepIdentifier}].{direction}.sql'.</param>
        /// <param name="direction">Direction of the migration operation.</param>
        /// <param name="stepIdentifier">Optional step identifer.</param>
        /// <param name="preamble">Optional lines of 'preamble' SQL to run before EXECUTE(). Useful for setting toggles, e.g. SET QUOTED_IDENTIFIER ON, etc</param>
        /// <returns>A builder to allow annotations to be added to the operation.</returns>
        public static OperationBuilder<SqlOperation> SqlFromFileWithExecute(this MigrationBuilder migrationBuilder, string scriptIdentifier, MigrationDirection direction, string stepIdentifier = null, IList<string> preamble = null)
        {
            var sql = GetSqlFromFile(scriptIdentifier, direction, stepIdentifier);

            sql = sql.Replace("'", "''"); // Escape embedded single quotes.

            if (preamble?.Any() ?? false)
            {
                sql = string.Join(Environment.NewLine, preamble) + Environment.NewLine + $"EXECUTE('{sql}');";
            }
            else
            {
                sql = $"EXECUTE('{sql}');";
            }

            return migrationBuilder.Sql(sql);
        }

        private static string GetSqlFromFile(string scriptIdentifier, MigrationDirection direction, string stepIdentifier = null)
        {
            if (!string.IsNullOrEmpty(stepIdentifier))
            {
                return File.ReadAllText($@"Scripts/{scriptIdentifier}/{scriptIdentifier}.{stepIdentifier}.{direction}.sql");
            }
            else
            {
                return File.ReadAllText($@"Scripts/{scriptIdentifier}/{scriptIdentifier}.{direction}.sql");
            }
        }

    }
}
