using System;
using NIHR.Infrastructure.Interfaces;

namespace NIHR.Infrastructure.Services;

public class UkTimezoneProvider : ITimezoneProvider
{
    public TimeZoneInfo GetCurrentTimezone()
    {
        return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
    }
}