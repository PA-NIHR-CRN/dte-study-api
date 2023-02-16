using System;
using FluentValidation;
using StudyApi.Requests.Participants;

namespace StudyApi.Validation.Participants
{
    public class CreateParticipantDemographicsRequestValidator : AbstractValidator<CreateParticipantDemographicsRequest>
    {
        public CreateParticipantDemographicsRequestValidator()
        {
            RuleFor(x => x.Address).SetValidator(new ParticipantAddressRequestValidator()).When(x => x.Address != null);
            RuleFor(x => x.DateOfBirth).NotEmpty().GreaterThan(DateTime.MinValue);
            RuleFor(x => x.SexRegisteredAtBirth).NotEmpty();
            RuleFor(x => x.EthnicGroup).NotEmpty();
            RuleFor(x => x.EthnicBackground).NotEmpty();
        }
    }
}