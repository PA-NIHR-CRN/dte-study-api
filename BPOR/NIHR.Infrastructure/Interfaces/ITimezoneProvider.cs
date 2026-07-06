using System;

namespace NIHR.Infrastructure.Interfaces;

public interface ITimezoneProvider
{
    TimeZoneInfo GetCurrentTimezone();
}