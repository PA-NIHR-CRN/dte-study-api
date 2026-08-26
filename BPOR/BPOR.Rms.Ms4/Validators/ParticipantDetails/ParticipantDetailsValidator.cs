using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.ParticipantDetails;

public class ParticipantDetailsValidator : AbstractValidator<ParticipantDetailsViewModel>
{
    public ParticipantDetailsValidator()
    {
        RuleFor(model => model.InclusionCriteria)
            .NotEmpty()
            .WithMessage("Enter inclusion criteria for this study");
    }
}