using FluentValidation;
using StudyApi.Requests.Researchers;

namespace StudyApi.Validation.Researchers
{
    public class GetResearcherFromIdgRequestValidator : AbstractValidator<GetResearcherFromIdgRequest>
    {
        public GetResearcherFromIdgRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}