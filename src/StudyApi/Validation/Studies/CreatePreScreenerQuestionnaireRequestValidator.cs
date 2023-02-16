using FluentValidation;
using StudyApi.Requests.Studies;

namespace StudyApi.Validation.Studies
{
    public class CreatePreScreenerQuestionnaireRequestValidator : AbstractValidator<CreatePreScreenerQuestionnaireRequest>
    {
        public CreatePreScreenerQuestionnaireRequestValidator()
        {
            RuleFor(x => x.Version).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Questions).NotNull().NotEmpty();
            RuleForEach(x => x.Questions).SetValidator(new CreatePreScreenerQuestionRequestValidator());
        }
    }

    public class CreatePreScreenerQuestionRequestValidator : AbstractValidator<CreatePreScreenerQuestionRequest>
    {
        public CreatePreScreenerQuestionRequestValidator()
        {
            RuleFor(x => x.QuestionText).NotEmpty();
            RuleFor(x => x.Reference).NotEmpty();
            RuleFor(x => x.AnswerOptionType).Must(x => x == "null" || x == "string" || x == "bool" || x == "number");
            RuleFor(x => x.AnswerOptions).NotEmpty();
            RuleFor(x => x.ValidAnswerOptions).NotEmpty();
            RuleFor(x => x.Sequence).GreaterThanOrEqualTo(1);
        }
    }
}