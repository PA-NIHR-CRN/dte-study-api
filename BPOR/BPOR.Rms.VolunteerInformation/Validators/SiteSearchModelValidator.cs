using BPOR.Rms.VolunteerInformation.Models;
using FluentValidation;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.VolunteerInformation.Validators;

public class SiteSearchModelValidator : AbstractValidator<SiteSearchModel>
{
    public SiteSearchModelValidator()
    {
        RuleFor(i => i.SearchTerm)
            .NotEmpty().WithMessage("Enter a postcode before submitting.")
            .Postcode().WithMessage("Enter a valid UK postcode");
    }
}

public class SiteSearchResultsModelValidator : AbstractValidator<SiteSearchResultsModel>
{
    public SiteSearchResultsModelValidator()
    {
        RuleFor(i => i.SearchTerm)
            .NotEmpty().WithMessage("Enter a postcode before submitting.")
            .Postcode().WithMessage("Enter a valid UK postcode");
        RuleFor(i => i.SelectedRtsId)
            .NotNull().WithMessage("Select an option to continue");
    }
}