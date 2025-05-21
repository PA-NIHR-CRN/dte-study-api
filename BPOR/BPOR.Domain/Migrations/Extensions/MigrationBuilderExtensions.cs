using Microsoft.EntityFrameworkCore.Migrations;
using System.Text.RegularExpressions;

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
        /// Runs a specific step script by index, based on file ordering.
        /// </summary>
        public static void RunSqlStep(this MigrationBuilder migrationBuilder, string scriptIdentifier, MigrationDirection direction, int index)
        {
            var orderedFiles = GetOrderedStepFiles(scriptIdentifier, direction).ToList();

            CheckForDuplicateSteps(orderedFiles, scriptIdentifier, direction);

            if (index < 0 || index >= orderedFiles.Count)
                return;

            var sql = ReadFileContents(orderedFiles[index]);
            if (!string.IsNullOrWhiteSpace(sql))
            {
                migrationBuilder.Sql(sql);
            }
        }

        /// <summary>
        /// Runs the common SQL file (e.g. 20240519_Migration.up.sql) if it exists.
        /// </summary>
        public static void RunCommonSql(this MigrationBuilder migrationBuilder, string scriptIdentifier, MigrationDirection direction)
        {
            var sql = ReadFileContents(GetCommonSqlFilePath(scriptIdentifier, direction));
            if (!string.IsNullOrWhiteSpace(sql))
            {
                migrationBuilder.Sql(sql);
            }
        }

        /// <summary>
        /// Throws an error if duplicate step identifiers exist (e.g. .1.up.sql used more than once).
        /// Call at the start of your migration method to prevent unsafe runs.
        /// </summary>
        public static void CheckForDuplicateSteps(string scriptIdentifier, MigrationDirection direction)
        {
            var files = GetOrderedStepFiles(scriptIdentifier, direction).ToList();
            CheckForDuplicateSteps(files, scriptIdentifier, direction);
        }

        private static void CheckForDuplicateSteps(IEnumerable<string> files, string scriptIdentifier, MigrationDirection direction)
        {
            var suffix = GetDirectionSuffix(direction);
            var pattern = $@"^{Regex.Escape(scriptIdentifier)}\.(.+?)\.{suffix}\.sql$";

            var duplicates = files
                .Select(Path.GetFileName)
                .Select(name => Regex.Match(name, pattern, RegexOptions.IgnoreCase))
                .Where(m => m.Success)
                .GroupBy(m => m.Groups[1].Value.ToLowerInvariant())
                .Where(g => g.Count() > 1)
                .ToList();

            if (duplicates.Any())
            {
                var steps = string.Join(", ", duplicates.Select(g => g.Key));
                throw new InvalidOperationException(
                    $"Migration error: Duplicate SQL step identifiers found for '{scriptIdentifier}.{direction}'");
            }
        }

        private static string GetScriptsDirectory(string scriptIdentifier)
        {
            var projectRoot = Directory.GetCurrentDirectory();
            return Path.Combine(projectRoot, "Migrations", "Scripts", scriptIdentifier);
        }

        private static string GetDirectionSuffix(MigrationDirection direction) =>
            direction.ToString().ToLowerInvariant();

        private static string GetCommonSqlFilePath(string scriptIdentifier, MigrationDirection direction)
        {
            var dir = GetScriptsDirectory(scriptIdentifier);
            var file = $"{scriptIdentifier}.{GetDirectionSuffix(direction)}.sql";
            return Path.Combine(dir, file);
        }

        private static string ReadFileContents(string path) =>
            File.Exists(path) ? File.ReadAllText(path) : string.Empty;

        private static IEnumerable<string> GetOrderedStepFiles(string scriptIdentifier, MigrationDirection direction)
        {
            var basePath = GetScriptsDirectory(scriptIdentifier);
            if (!Directory.Exists(basePath))
                return Enumerable.Empty<string>();

            var suffix = GetDirectionSuffix(direction);
            var pattern = $@"^{Regex.Escape(scriptIdentifier)}\.(.+?)\.{suffix}\.sql$";

            return Directory
                .EnumerateFiles(basePath, $"*.{suffix}.sql", SearchOption.TopDirectoryOnly)
                .Select(f => new
                {
                    Path = f,
                    Match = Regex.Match(Path.GetFileName(f), pattern, RegexOptions.IgnoreCase)
                })
                .Where(x => x.Match.Success && !x.Match.Groups[1].Value.Equals(suffix, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.Match.Groups[1].Value, StringComparer.OrdinalIgnoreCase)
                .Select(x => x.Path);
        }
    }
}