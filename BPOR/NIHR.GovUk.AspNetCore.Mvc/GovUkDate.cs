﻿using System.ComponentModel.DataAnnotations;

namespace NIHR.GovUk.AspNetCore.Mvc;

public class GovUkDate : IValidatableObject
{

    private readonly int _minYear;
    private readonly int _maxYear;

    public GovUkDate(int minYear, int maxYear)
    {
        _minYear = minYear;
        _maxYear = maxYear;
    }

    [Display(Name = "Day")]
    [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
    public int? Day { get; set; }

    [Display(Name = "Month")]
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
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

    public static GovUkDate FromDateTime(DateTime? date) => new GovUkDate(1900,2100) { Day = date?.Day, Month = date?.Month, Year = date?.Year };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!HasAnyDateComponent) { yield break; }

        if (!Day.HasValue)
        {
            yield return new ValidationResult($"{validationContext.DisplayName} must include a day.", [nameof(Day)]);
        }

        if (!Month.HasValue)
        {
            yield return new ValidationResult($"{validationContext.DisplayName} must include a month.", [nameof(Month)]);
        }

        if (!Year.HasValue)
        {
            yield return new ValidationResult($"{validationContext.DisplayName} must include a year.", [nameof(Year)]);
        }

        if (Day.HasValue && Month.HasValue && Year.HasValue)
        {
            if (Year < _minYear || Year > _maxYear)
            {
                yield return new ValidationResult($"{validationContext.DisplayName} year must be a reasonable value.", new[] { nameof(Year) });
            }
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
