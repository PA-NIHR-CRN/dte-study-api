namespace BPOR.Domain.Utils;

public static class AgeUtils
{
    public static bool IsUnder18(DateTime dateOfBirth) => DateTime.Now.AddYears(-18).Date < dateOfBirth.Date;
}
