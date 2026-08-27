using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Details;

public class ChiefInvestigatorValidator : AbstractValidator<StudyRequestViewModel>
{
    public ChiefInvestigatorValidator()
    {
        RuleFor(model => model.ChiefInvestigatorName)
            .NotNull()
            .WithMessage("Enter a name");
        
        RuleFor(model => model.ChiefInvestigatorEmail)
            .NotNull()
            .WithMessage("Enter an email")
            .EmailAddress()
            .WithMessage("The email provided isn’t in the right format");
    }
}