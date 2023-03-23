using FluentValidation;
using StudyApi.Requests.Users;

namespace StudyApi.Validation.Users
{
    public class ConfirmSignUpRequestValidator : AbstractValidator<ConfirmSignUpRequest>
    {
        public ConfirmSignUpRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        }
    }
}