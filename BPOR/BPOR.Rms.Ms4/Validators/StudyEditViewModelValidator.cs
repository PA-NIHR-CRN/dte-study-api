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
       
       RuleFor(model => model.IsRecruitingIdentifiableParticipants)
           .NotNull().WithMessage("Select an option");
       
       RuleFor(model => model.RecruitmentTarget)
           .NotEmpty().WithMessage("Enter a recruitment target");
    }
}