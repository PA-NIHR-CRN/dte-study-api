using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Details;

public class MainContactValidator : AbstractValidator<StudyDetailsViewModel>
{
    public MainContactValidator()
    {
        RuleFor(model => model.MainContactName)
            .NotNull()
            .WithMessage("Enter a name");
        
        RuleFor(model => model.MainContactRole)
            .NotNull()
            .WithMessage("Enter a name");
        
        RuleFor(model => model.MainContactEmail)
            .NotNull()
            .WithMessage("Enter an email")
            .EmailAddress()
            .WithMessage("The email provided isn’t in the right format");
    }
}