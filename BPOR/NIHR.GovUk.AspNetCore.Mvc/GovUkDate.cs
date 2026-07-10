using System.ComponentModel.DataAnnotations;

namespace NIHR.GovUk.AspNetCore.Mvc;

public class GovUkDate
{

    [Display(Name = "Day")]
    public int? Day { get; set; }

    [Display(Name = "Month")]
    public int? Month { get; set; }

    [Display(Name = "Year")]
    public int? Year { get; set; }

    public bool HasValue => Day.HasValue && Month.HasValue && Year.HasValue;

    public bool HasAnyDateComponent => Day.HasValue || Month.HasValue || Year.HasValue;

    public DateOnly? ToDateOnly()
    {
        if (!HasValue)
        {
            return null;
        }

        try
        {
            return new DateOnly(Year.GetValueOrDefault(), Month.GetValueOrDefault(), Day.GetValueOrDefault());
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }

    public static GovUkDate FromDateTime(DateTime? date) => new GovUkDate() { Day = date?.Day, Month = date?.Month, Year = date?.Year };
    
    private static string? formatDateForUK(int? year, int? month, int? day)
    {
        if (!year.HasValue || !month.HasValue || !day.HasValue)
        {
            return null;
        }

        return "" + day.Value + "/" + month.Value + "/" + year.Value;
    }


    public string? UKDisplayDate() => formatDateForUK(Year, Month, Day);
}
