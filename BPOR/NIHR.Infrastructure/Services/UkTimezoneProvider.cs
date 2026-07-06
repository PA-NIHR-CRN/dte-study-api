using System;
using System.Linq;
using NIHR.Infrastructure.Interfaces;

namespace NIHR.Infrastructure.Services;

public class UkTimezoneProvider : ITimezoneProvider
{
    private TimeZoneInfo _timezone;
    
    public UkTimezoneProvider()
    {
        string[] desiredTimezoneNames = ["GMT Standard Time", "Europe/London"];
        var availableTimeZones = TimeZoneInfo.GetSystemTimeZones();
        _timezone = availableTimeZones.FirstOrDefault(i => desiredTimezoneNames.Contains(i.StandardName)) ?? TimeZoneInfo.Utc;
    }

    public TimeZoneInfo GetCurrentTimezone()
    {
        return _timezone;
    }
}