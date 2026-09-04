using BPOR.Rms.Ms4.Models;
using FluentValidation;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;

namespace BPOR.Rms.Ms4.Validators.Details;

public class ChiefInvestigatorValidator : AbstractValidator<StudyRequestViewModel>
{
    public ChiefInvestigatorValidator()
    {
        RuleFor(model => model.ChiefInvestigatorName)
            .NotNull()
            .WithMessage("Enter a name")
            .MaximumLength(PropertyBuilderExtensions.NameMaxLength)
            .WithMessage($"Chief investigation name must be {PropertyBuilderExtensions.NameMaxLength} characters or less");
        
        RuleFor(model => model.ChiefInvestigatorEmail)
            .NotNull()
            .WithMessage("Enter an email")
            .EmailAddress()
            .WithMessage("The email provided isn’t in the right format");
    }
}