using FluentValidation;
using StudyApi.Requests.Participants;

namespace StudyApi.Validation.Participants
{
    public class UpdateParticipantDetailsRequestValidator : AbstractValidator<UpdateParticipantDetailsRequest>
    {
        public UpdateParticipantDetailsRequestValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Lastname).NotEmpty();
        }
    }
}