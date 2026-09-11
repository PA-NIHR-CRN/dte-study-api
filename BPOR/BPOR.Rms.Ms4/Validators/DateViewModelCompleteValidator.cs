using BPOR.Rms.Ms4.Models;
using FluentValidation;
using NIHR.GovUk.AspNetCore.Mvc;

namespace BPOR.Rms.Ms4.Validators;

public class DateViewModelCompleteValidator : AbstractValidator<GovUkDate>
{
    public DateViewModelCompleteValidator()
    {
        RuleFor(x => x)
            .Custom((value, context) =>
            {
                if (!value.HasValue)
                {
                    List<string> missingTerms = new();
                    if (!value.Day.HasValue)
                    {
                        missingTerms.Add("day");
                    }                 
                    if (!value.Month.HasValue)
                    {
                        missingTerms.Add("month");
                    }
                    if (!value.Year.HasValue)
                    {
                        missingTerms.Add("year");
                    }

                    context.AddFailure($"Enter a {StringHelper.Join(", ", " and ", missingTerms)}");
                }
            });
    }
}