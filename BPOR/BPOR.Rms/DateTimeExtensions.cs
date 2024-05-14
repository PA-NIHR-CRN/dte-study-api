namespace BPOR.Rms
{
    public static class DateTimeExtensions
    {

        public static int? YearsTo(this DateTime? birthDate, DateTime onDate)
        {
            if (!birthDate.HasValue)
            {
                return null;
            }

            int age = onDate.Year - birthDate.Value.Year;

            // Go back to the year in which the person was born.
            // If birthDate hasn't arrived yet, subtract one year.
            // Also handles leap year.
            if (birthDate.Value.Date > onDate.AddYears(-age))
            {
                age--;
            }

            if (age < 0)
            {
                return null;
            }

            return age;
        }
    }
}
