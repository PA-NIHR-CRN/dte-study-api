using BPOR.Rms.Ms4.Models;
using FluentValidation;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;

namespace BPOR.Rms.Ms4.Validators.Details;

public class MainContactValidator : AbstractValidator<StudyRequestViewModel>
{
    public MainContactValidator()
    {
        RuleFor(model => model.MainContactName)
            .NotNull()
            .WithMessage("Enter a name")
            .MaximumLength(PropertyBuilderExtensions.NameMaxLength)
            .WithMessage($"Main contact name must be {PropertyBuilderExtensions.NameMaxLength} characters or less");
        
        RuleFor(model => model.MainContactRole)
            .NotNull()
            .WithMessage("Enter a role");
        
        RuleFor(model => model.MainContactEmail)
            .NotNull()
            .WithMessage("Enter an email")
            .EmailAddress()
            .WithMessage("The email provided isn’t in the right format");
    }
}