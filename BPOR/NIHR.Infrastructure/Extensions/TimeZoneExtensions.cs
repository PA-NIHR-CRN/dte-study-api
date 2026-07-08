using System;
using NIHR.Infrastructure.Interfaces;

namespace NIHR.Infrastructure;

public static class TimeZoneExtensions
{
    public static DateTime UtcToLocalTime(this ITimezoneProvider timezoneProvider, DateTime utc) 
        => TimeZoneInfo.ConvertTimeFromUtc(utc, timezoneProvider.GetCurrentTimezone());
}