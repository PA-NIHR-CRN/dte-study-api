using BPOR.Rms.VolunteerInformation.Models;
using FluentValidation;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.VolunteerInformation.Validators;

public class VsiContactModelValidator : AbstractValidator<VsiContactModel>
{
    public VsiContactModelValidator()
    {
        RuleFor(i => i.PhoneNumber)
            .MaximumLength(32).WithMessage("You can add up to 32 characters")
            .NotNull().WithMessage("You must enter a UK phone number.");
        RuleFor(i => i.Name)
            .MaximumLength(100).WithMessage("You can add up to 100 characters");
        RuleFor(i => i.Organisation)
            .MaximumLength(100).WithMessage("You can add up to 100 characters");
        RuleFor(i => i.Email)
            .EmailAddress();
        RuleFor(i => i.Role)
            .MaximumLength(100).WithMessage("You can add up to 100 characters");
    }
}

public class ManualSiteEntryModelValidator : AbstractValidator<ManualSiteEntryModel>
{
    public ManualSiteEntryModelValidator()
    {
        RuleFor(i => i.Address.AddressLine1)
            .MaximumLength(100).WithMessage("You can add up to 100 characters")
            .NotNull().WithMessage("Enter an address line one before submitting.");
        RuleFor(i => i.Address.AddressLine2)
            .MaximumLength(100).WithMessage("You can add up to 100 characters");
        RuleFor(i => i.Address.AddressLine3)
            .MaximumLength(100).WithMessage("You can add up to 100 characters");
        RuleFor(i => i.Address.AddressLine4)
            .MaximumLength(100).WithMessage("You can add up to 100 characters")
            .NotNull().WithMessage("Enter town or city before submitting.");
        RuleFor(i => i.Address.AddressLine5)
            .MaximumLength(100).WithMessage("You can add up to 100 characters");
        RuleFor(i => i.Address.Postcode)
            .MaximumLength(8).WithMessage("You can add up to 8 characters")
            .NotNull().WithMessage("Enter an post code before submitting.");
    }
}

public class VsiValidator : AbstractValidator<VsiEditModel>
{
    public VsiValidator()
    {
        RuleFor(i => i)
            .Must(i => i.Contacts.Any() || !string.IsNullOrWhiteSpace(i.StagedPreScreenerUrl) ||
                       !string.IsNullOrWhiteSpace(i.ExternalWebsiteUrl))
            .WithMessage(
                "You must provide at least one contact method for the volunteer to get in touch with the study team.");
            
        RuleFor(i => i.Description)
            .NotEmpty().WithMessage("You must enter a description of your study.")
            .MaxWords(60).WithMessage("You can add up to 60 words.");
        RuleFor(i => i.StudyType)
            .NotNull().WithMessage("Select an option to continue");
        RuleFor(i => i.WhatYouWillDo)
            .MaximumLength(200).WithMessage("You have entered more than 200 characters")
            .NotNull().WithMessage("Provide a brief description of what the volunteer will be doing as part of the study.");
        RuleFor(i => i.CostReimbursement)
            .NotNull().WithMessage("Select an option to continue");
        RuleFor(i => i.HasIncentive)
            .NotNull().WithMessage("Select an option to continue");
        RuleFor(i => i.IncentiveDetails)
            .MaximumLength(200).WithMessage("You can add up to 200 characters")
            .NotNull().WithMessage("Provide details of the incentive that will be provided to the volunteer.\n")
            .When(i => i.HasIncentive == true);
        RuleFor(i => i.NumberOfVisits)
            .MaximumLength(200).WithMessage("You can add up to 200 characters")
            .NotNull().WithMessage("Provide the number of times a person would need to visit the place of research.");
        RuleFor(i => i.StudyDuration)
            .MaximumLength(200).WithMessage("You can add up to 200 characters")
            .NotNull().WithMessage("Provide details of how long this study is expected to last. This can be days, months, years, or specific dates (if any).");
        RuleFor(i => i.StudyFormat)
            .MaximumLength(200).WithMessage("You can add up to 200 characters")
            .NotNull().WithMessage("Describe the different stages of the study, including treatments and follow-up appointments.");
        RuleFor(i => i.OtherDetails)
            .MaximumLength(200).WithMessage("You can add up to 200 characters")
            /*.NotNull().WithMessage("Enter information to Continue. If this is not relevant to your study, Skip this question")*/;
        RuleFor(i => i.StagedPreScreenerUrl)
            .Uri()
            /*.NotNull().WithMessage("Enter information to Continue. If this is not relevant to your study, Skip this question")*/;
        RuleFor(i => i.ExternalWebsiteUrl)
            .Uri()
            /*.NotNull().WithMessage("Enter information to Continue. If this is not relevant to your study, Skip this question")*/;
        RuleFor(i => i.InfoToRegisterByEmail)
            .MaximumLength(200).WithMessage("You can add up to 200 characters")
            /*.NotNull().WithMessage("Enter information to Continue. If this is not relevant to your study, Skip this question")*/;
    }
}