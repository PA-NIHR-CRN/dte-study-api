using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Sponsorship;

public class SponsorOrganisationValidator : AbstractValidator<SponsorshipViewModel>
{
    public SponsorOrganisationValidator()
    {
        RuleFor(model => model.SponsorName)
            .NotEmpty()
            .WithMessage("Enter a sponsor name to continue");
    }
}