using FluentValidation;
using StudyApi.Requests.Participants;

namespace StudyApi.Validation.Participants
{
    public class ParticipantAddressRequestValidator : AbstractValidator<CreateParticipantAddressRequest>
    {
        public ParticipantAddressRequestValidator()
        {
            RuleFor(x => x.AddressLine1).NotEmpty();
            RuleFor(x => x.Town).NotEmpty();
        }
    }
}