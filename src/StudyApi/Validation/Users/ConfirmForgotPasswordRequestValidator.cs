using FluentValidation;
using StudyApi.Requests.Users;

namespace StudyApi.Validation.Users
{
    public class ConfirmForgotPasswordRequestValidator : AbstractValidator<ConfirmForgotPasswordRequest>
    {
        public ConfirmForgotPasswordRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}