using BPOR.Domain.Enums;
using BPOR.Rms.Ms4.Models;
using FluentValidation;
using NIHR.GovUk.AspNetCore.Mvc.Validation;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.Ms4.Validators;

public class StudyEditViewModelValidator : AbstractValidator<StudyEditViewModel>
{
    public StudyEditViewModelValidator()
    {
       RuleFor(model => model.RecruitmentStartDate)
            .IsComplete()
            .IsValidDate().WithMessage("Enter a real date");

       RuleFor(model => model.InformationUrl)
           .Uri();
       
       RuleFor(model => model.PreScreenerUrl)
           .NotEmpty()
           .Uri();
       
       RuleFor(model => model.IsRecruitingIdentifiableParticipants)
           .NotNull().WithMessage("Select an option");
       
       RuleFor(model => model.RecruitmentTarget)
           .NotEmpty().WithMessage("Enter a recruitment target");
       
       RuleFor(model => model.Status)
           .NotEmpty()
           .WithMessage("Select an option")
           .NotEqual(model => model.CurrentStatus)
           .WithMessage("Select a different status");
       
       RuleFor(model => model.RejectedReasons)
           .NotEmpty().WithMessage("Select an option");
       
       RuleFor(model => model.RejectedReasons)
           .NotEmpty()
           .WithMessage("Enter a reason")
           .When(model => model.RejectedReasons.Contains(RejectedReasonType.Misc));
       
       RuleFor(model => model.WithdrawnReasons)
           .NotEmpty().WithMessage("Select an option");
       
       RuleFor(model => model.WithdrawnOtherReason)
           .NotEmpty()
           .WithMessage("Enter a reason")
           .When(model => model.WithdrawnReasons.Contains(WithdrawnReasonType.Other));
    }
}