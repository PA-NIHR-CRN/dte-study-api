using FluentValidation;
using StudyApi.Requests.StudyRegistrations;
using StudyApi.Validation.Studies;

namespace StudyApi.Validation.StudyRegistrations
{
    public class CreateStudyRegistrationRequestValidator : AbstractValidator<CreateStudyRegistrationRequest>
    {
        public CreateStudyRegistrationRequestValidator()
        {
            RuleFor(x => x.StudyId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Researcher).NotNull().SetValidator(new ResearcherRequestValidator());
        }
    }
}