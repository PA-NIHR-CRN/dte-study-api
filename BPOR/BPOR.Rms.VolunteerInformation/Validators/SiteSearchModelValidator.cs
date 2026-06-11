using BPOR.Rms.VolunteerInformation.Models;
using FluentValidation;

namespace BPOR.Rms.VolunteerInformation.Validators;

public class SiteSearchModelValidator : AbstractValidator<SiteSearchModel>
{
    public SiteSearchModelValidator()
    {
        RuleFor(i => i.SearchTerm)
            .NotEmpty().WithMessage("Enter a postcode before submitting.");
        RuleFor(i => i.SelectedRtsId)
            .NotNull().WithMessage("Select an option to continue");
    }
}