using System;

namespace BPOR.Rms.Infrastructure.Interfaces;

public interface ITimezoneProvider
{
    TimeZoneInfo GetCurrentTimezone();
}