using FluentAssertions;
using Microsoft.Extensions.Logging.Testing;

namespace NIHR.NotificationService.Tests.Helpers;

internal static class LogHelperMethods
{
    public static bool IsError(this FakeLogRecord logRecord) => logRecord.Level == Microsoft.Extensions.Logging.LogLevel.Error;
    public static bool IsWarning(this FakeLogRecord logRecord) => logRecord.Level == Microsoft.Extensions.Logging.LogLevel.Warning;

    public static IEnumerable<FakeLogRecord> Errors(this IEnumerable<FakeLogRecord> log) => log.Where(IsError);
    public static IEnumerable<FakeLogRecord> Warnings(this IEnumerable<FakeLogRecord> log) => log.Where(IsWarning);
}
