using BPOR.Rms.VolunteerInformation.Models;
using FluentValidation;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.VolunteerInformation.Validators;

public class SiteSearchResultsModelValidator : AbstractValidator<SiteSearchModel>
{
    public SiteSearchResultsModelValidator()
    {
        RuleFor(i => i.SearchTerm)
            .NotEmpty().WithMessage("Enter a postcode before submitting.")
            .Postcode().WithMessage("Enter a valid UK postcode")
            .When(i => i.IsSearching);
        RuleFor(i => i.SelectedRtsId)
            .NotNull().WithMessage("Select an option to continue");
    }
}