using FluentValidation;
using StudyApi.Requests.ResearcherStudies;

namespace StudyApi.Validation.ResearcherStudies
{
    public class CreateResearcherStudyRequestValidator : AbstractValidator<CreateResearcherStudyRequest>
    {
        public CreateResearcherStudyRequestValidator()
        {
            RuleFor(x => x.ResearcherId).NotEmpty();
            RuleFor(x => x.ResearcherFirstname).NotEmpty();
            RuleFor(x => x.ResearcherLastname).NotEmpty();
            RuleFor(x => x.ResearcherEmail).NotEmpty();
            RuleFor(x => x.StudyId).NotEmpty();
            RuleFor(x => x.Role).NotEmpty();
        }
    }
}