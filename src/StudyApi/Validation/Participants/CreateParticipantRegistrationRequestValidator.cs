using FluentValidation;
using StudyApi.Requests.Participants;

namespace StudyApi.Validation.Participants
{
    public class CreateParticipantRegistrationRequestValidator : AbstractValidator<CreateParticipantRegistrationRequest>
    {
        public CreateParticipantRegistrationRequestValidator()
        {
            RuleFor(x => x.StudyId).NotEmpty();
            RuleFor(x => x.SiteId).NotEmpty();
            RuleFor(x => x.ParticipantId).NotEmpty();
        }
    }
}