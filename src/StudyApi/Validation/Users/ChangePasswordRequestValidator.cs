using FluentValidation;
using StudyApi.Requests.Users;

namespace StudyApi.Validation.Users;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.NewPassword).NotEmpty();
    }
}
