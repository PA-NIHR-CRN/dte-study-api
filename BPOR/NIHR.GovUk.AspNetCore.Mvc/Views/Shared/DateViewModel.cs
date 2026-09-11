namespace BPOR.Rms.Ms4.Models;

public class DateViewModel
{
    public int? Day { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
    
    public bool IsComplete => Day.HasValue && Month.HasValue && Year.HasValue;

    public DateTime ToDateTime() => new(Year!.Value, Month!.Value, Day!.Value);

    public static DateViewModel FromDateTime(DateTime? value)
    {
        return value == null ? new DateViewModel() : new DateViewModel(){Year =  value.Value.Year, Month = value.Value.Month, Day = value.Value.Day};
    }
}