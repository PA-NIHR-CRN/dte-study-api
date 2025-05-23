using FluentAssertions;
using FluentAssertions.Primitives;

namespace NIHR.NotificationService.Tests.Helpers;

internal static class AssertionHelperMethods
{
    public static AndConstraint<NullableDateTimeAssertions> BeRecentUtc(this NullableDateTimeAssertions assertions)
    {
        DateTime utcNow = DateTime.UtcNow;
        return assertions.BeAfter(utcNow.AddMinutes(-5)).And.BeOnOrBefore(utcNow);
    }
}
