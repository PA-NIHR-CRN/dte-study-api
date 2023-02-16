using FluentValidation;
using StudyApi.Requests.Studies;

namespace StudyApi.Validation.Studies
{
    public class ResearcherRequestValidator : AbstractValidator<ResearcherRequest>
    {
        public ResearcherRequestValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Lastname).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}