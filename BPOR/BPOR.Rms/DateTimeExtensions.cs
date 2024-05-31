namespace BPOR.Rms
{
    public static class DateTimeExtensions
    {
        public static int? YearsTo(this DateTime? sourceDate, DateTime onDate) => YearsTo(sourceDate.HasValue ? DateOnly.FromDateTime(sourceDate.Value) : null, DateOnly.FromDateTime(onDate));

        public static int? YearsTo(this DateTime? sourceDate, DateOnly onDate) => YearsTo(sourceDate.HasValue ? DateOnly.FromDateTime(sourceDate.Value) : null, onDate);

        public static int? YearsTo(this DateOnly? sourceDate, DateOnly onDate)
        {
            if (!sourceDate.HasValue)
            {
                return null;
            }

            int age = onDate.Year - sourceDate.Value.Year;

            // Go back to the year in which the person was born.
            // If birthDate hasn't arrived yet, subtract one year.
            // Also handles leap year.
            if (sourceDate.Value > onDate.AddYears(-age))
            {
                age--;
            }

            if (age < 0)
            {
                return null;
            }

            return age;
        }

        public static DateOnlyRange GetDatesWithinYearRange(this DateTime originDate, int? minYears, int? maxYears) => DateOnly.FromDateTime(originDate).GetDatesWithinYearRange(minYears, maxYears);

        public static DateOnlyRange GetDatesWithinYearRange(this DateOnly originDate, int? minYears, int? maxYears)
        {
            DateOnly? earliestDate = maxYears.HasValue ? originDate.AddYears(-maxYears.Value - 1).AddDays(1) : null;

            DateOnly? latestDate = minYears.HasValue ? originDate.AddYears(-minYears.Value) : null;


            // Check the bounds of the range are correct.
            // These exceptions should never be thrown.
            if (maxYears.HasValue && YearsTo(earliestDate, originDate) != maxYears)
            {
                throw new InvalidOperationException($"Date range from date {earliestDate} is not {maxYears} from {originDate}.");
            }

            if (minYears.HasValue && YearsTo(latestDate, originDate) != minYears)
            {
                throw new InvalidOperationException($"Date range from date {latestDate} is not {minYears} from {originDate}.");
            }

            return new DateOnlyRange { From = earliestDate, To = latestDate };
        }
    }

    public struct DateOnlyRange
    {
        public DateOnly? From { get; set; }
        public DateOnly? To { get; set; }
    }
}
