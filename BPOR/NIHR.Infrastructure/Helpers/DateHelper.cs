using System;

namespace NIHR.Infrastructure;

public static class DateHelper
{
    public static bool IsValidDate(int year, int month, int day)
    {
        return
            year >= 1 && year <= 9999 &&
            month >= 1 && month <= 12 &&
            day >= 1 && day <= DateTime.DaysInMonth(year, month);
    }
}