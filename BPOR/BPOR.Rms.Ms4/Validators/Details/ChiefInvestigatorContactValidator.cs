using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Details;

public class ChiefInvestigatorContactValidator : AbstractValidator<StudyRequestViewModel>
{
    public ChiefInvestigatorContactValidator()
    {
        RuleFor(model => model.IsChiefInvestigatorMainContact)
            .NotNull()
            .WithMessage("Select an option");
    }
}