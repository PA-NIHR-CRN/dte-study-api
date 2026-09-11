using BPOR.Domain.Entities.Configuration;
using BPOR.Domain.Enums;
using BPOR.Rms.Ms4.Models;
using FluentValidation;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;

namespace BPOR.Rms.Ms4.Validators;

public class StudyRequestViewModelValidator : AbstractValidator<StudyRequestViewModel>
{
    public StudyRequestViewModelValidator()
    {
        #region Section 1
        RuleFor(model => model.HasEthicsApproval)
            .NotNull()
            .WithMessage("Select an option");
        
        RuleFor(model => model.InclusionInRdnPortfolioStatus)
            .NotNull()
            .WithMessage("Select an option");

        RuleFor(model => model.CpmsId)
            .NotEmpty()
            .WithMessage("Enter CPMS ID to continue")
            .When(model => model.InclusionInRdnPortfolioStatus == SubmittedType.Yes);
        
        RuleFor(model => model.NihrFundingStatus)
            .NotNull()
            .WithMessage("Select an option")
            .When(model => model.InclusionInRdnPortfolioStatus != SubmittedType.Yes);
        
        RuleFor(model => model.FinishRecruiting)
            .IsComplete()
            .IsValidDate().WithMessage("Enter a real date")
            .IsInFuture().WithMessage("End of recruitment date must be in the future");
        #endregion
        
        #region Section 2
        
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
        
        RuleFor(model => model.HasMultipleResearchLocations)
            .NotNull()
            .WithMessage("Select an option");
        
        RuleFor(model => model.SinglePersonResponsibleForRecruiting)
            .NotNull()
            .WithMessage("Select an option");
        
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
        #endregion
        
        #region section 3
        RuleFor(model => model.SponsorName)
            .NotEmpty()
            .WithMessage("Enter a sponsor name to continue");
        #endregion
        
        #region section 4
        RuleFor(model => model.InclusionCriteria)
            .NotEmpty()
            .WithMessage("Enter inclusion criteria for this study")
            .MaximumLength(StudyConfiguration.InclusionCriteriaMaxLength)
            .WithMessage($"You have entered more than {StudyConfiguration.InclusionCriteriaMaxLength} characters");
        #endregion
    }
}