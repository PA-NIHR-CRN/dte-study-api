using FluentValidation;
using StudyApi.Requests.StudyRegistrations;

namespace StudyApi.Validation.StudyRegistrations
{
    public class ApproveStudyRegistrationRequestValidator : AbstractValidator<ApproveStudyRegistrationRequest>
    {
        public ApproveStudyRegistrationRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}