using BPOR.Rms.Models.Study;
using FluentValidation;
using FluentValidation.Validators;

namespace BPOR.Rms.Validators;

public class StudyFormModelValidator : AbstractValidator<StudyFormEditModel>
{
    public StudyFormModelValidator()
    {
        RuleFor(i => i.FullName)
            .NotEmpty().WithMessage("Enter the name of the main contact for the study")
            .MaximumLength(255).WithMessage("Main contact name must be less than 256 characters");
        RuleFor(i => i.EmailAddress)
            .NotEmpty().WithMessage("Enter the email address of the main contact for the study")
            .EmailAddress().WithMessage("The email address you have entered is not in an accepted format. Example: name@example.com")
            .MaximumLength(255).WithMessage("Email address must be less than 256 characters");
        RuleFor(i => i.StudyName)
            .NotEmpty().WithMessage("Enter the study name")
            .MaximumLength(255).WithMessage("Study name must be less than 256 characters");
        RuleFor(i => i.IsRecruitingIdentifiableParticipants)
            .NotNull().WithMessage("Select whether the study is recruiting identifiable participants")
            .When(i => i.AllowEditIsRecruitingIdentifiableParticipants);
        RuleFor(i => i.InformationUrl)
            .UriOrNullOrWhitespace().WithMessage("The website you have tried to enter is not formatted correct")
            .MaximumLength(2048).WithMessage("Website must be less than 2049 characters");
    }
}