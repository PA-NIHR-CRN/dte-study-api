using BPOR.Domain.Entities.Configuration;
using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.ParticipantDetails;

public class ParticipantDetailsValidator : AbstractValidator<StudyRequestViewModel>
{
    public ParticipantDetailsValidator()
    {
        RuleFor(model => model.InclusionCriteria)
            .NotEmpty()
            .WithMessage("Enter inclusion criteria for this study");
        
        RuleFor(model => model.InclusionCriteria)
            .MaximumLength(StudyConfiguration.InclusionCriteriaMaxLength)
            .WithMessage("You have entered more than 500 characters");
    }
}