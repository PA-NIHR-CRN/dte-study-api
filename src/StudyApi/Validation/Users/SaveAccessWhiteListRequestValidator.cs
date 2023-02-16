using FluentValidation;
using StudyApi.Requests.Users;

namespace StudyApi.Validation.Users
{
    public class SaveAccessWhiteListRequestValidator : AbstractValidator<SaveAccessWhiteListRequest>
    {
        public SaveAccessWhiteListRequestValidator()
        {
            RuleFor(x => x.Emails).NotEmpty();
        }
    }
}