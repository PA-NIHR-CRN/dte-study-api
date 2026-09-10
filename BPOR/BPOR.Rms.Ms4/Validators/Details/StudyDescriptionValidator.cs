using BPOR.Domain.Entities.Configuration;
using BPOR.Rms.Ms4.Models;
using FluentValidation;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;

namespace BPOR.Rms.Ms4.Validators.Details;

public class StudyDescriptionValidator : AbstractValidator<StudyRequestViewModel>
{
    public StudyDescriptionValidator()
    {
        RuleFor(model => model.StudyTitle)
            .NotNull()
            .WithMessage("Enter a study title")
            .MaximumLength(PropertyBuilderExtensions.NameMaxLength)
            .WithMessage($"Study title must be {PropertyBuilderExtensions.NameMaxLength} characters or less");
        
        RuleFor(model => model.StudyDescription)
            .NotNull()
            .WithMessage("Provide a description")
            .MaximumLength(StudyConfiguration.DescriptionMaxLength)
            .WithMessage($"Study description must be {StudyConfiguration.DescriptionMaxLength} characters or less");
    }
}