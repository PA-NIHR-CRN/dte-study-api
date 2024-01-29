using System;

namespace Infrastructure.Helpers;

public static class AgeHelper
{
    public static bool IsUnder18(DateTime dateOfBirth) => DateTime.Now.AddYears(-18).Date < dateOfBirth.Date;
}
