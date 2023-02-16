using FluentValidation;
using StudyApi.Requests.Users;

namespace StudyApi.Validation.Users
{
    public class ResendVerificationEmailRequestValidator : AbstractValidator<ResendVerificationEmailRequest>
    {
        public ResendVerificationEmailRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        }
    }
}