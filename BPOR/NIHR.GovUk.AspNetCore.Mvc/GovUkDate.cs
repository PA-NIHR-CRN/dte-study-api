using System.ComponentModel.DataAnnotations;

namespace NIHR.GovUk.AspNetCore.Mvc;

public class GovUkDate : IValidatableObject
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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if (!Day.HasValue) {
            if(Month.HasValue && Year.HasValue)
            {
                yield return new ValidationResult($"{validationContext.DisplayName} must include a day", [nameof(Day)]);
            }
            if (Month.HasValue && !Year.HasValue)
            {
                yield return new ValidationResult($"{validationContext.DisplayName} must include a day and year", [nameof(Day)]);
            }
            if (!Month.HasValue && Year.HasValue)
            {
                yield return new ValidationResult($"{validationContext.DisplayName} must include a day and month", [nameof(Day)]);
            }

        }
        if (!Month.HasValue)
        {
            if(Day.HasValue && Year.HasValue)
            {
                yield return new ValidationResult($"{validationContext.DisplayName} must include a month.", [nameof(Month)]);
            }
            if(Day.HasValue && !Year.HasValue)
            {
                yield return new ValidationResult($"{validationContext.DisplayName} must include a month and year", [nameof(Month)]);
            }
        }

        if (!Year.HasValue)
        {
            if(Day.HasValue && Month.HasValue)
            {
                yield return new ValidationResult($"{validationContext.DisplayName} must include a year", [nameof(Year)]);
            }
        }

        if (Day.HasValue && (Day > 31 || Day < 1))
        {
            yield return new ValidationResult($"Date of birth day must be a real day", [nameof(Day)]);
        }

        if (Month.HasValue && (Month > 12 || Month < 1))
        {
            yield return new ValidationResult($"Date of birth month must be a real month", [nameof(Month)]);
        }
            
        if(Year.HasValue && (Year < 1000 || Year > 9999))               
        {
            yield return new ValidationResult($"Date of birth year must include 4 numbers", [nameof(Year)]);
        }
        
    }

   
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
