using System;
using BPOR.Rms.Infrastructure.Interfaces;

namespace BPOR.Rms.Infrastructure;

public static class TimeZoneExtensions
{
    public static DateTime UtcToLocalTime(this ITimezoneProvider timezoneProvider, DateTime utc) 
        => TimeZoneInfo.ConvertTimeFromUtc(utc, timezoneProvider.GetCurrentTimezone());
}