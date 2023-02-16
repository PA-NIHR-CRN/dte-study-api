using FluentValidation;
using StudyApi.Requests.Users;

namespace StudyApi.Validation.Users
{
    public class DeleteParticipantAccountRequestValidator : AbstractValidator<DeleteParticipantAccountRequest>
    {
        public DeleteParticipantAccountRequestValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty();
        }
    }
}