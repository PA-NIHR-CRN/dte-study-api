using System;
using FluentValidation;
using StudyApi.Requests.Participants;

namespace StudyApi.Validation.Participants
{
    public class UpdateParticipantDemographicsRequestValidator : AbstractValidator<UpdateParticipantDemographicsRequest>
    {
        public UpdateParticipantDemographicsRequestValidator()
        {
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty().GreaterThan(DateTime.MinValue);
            RuleFor(x => x.SexRegisteredAtBirth).NotEmpty();
            RuleFor(x => x.EthnicGroup).NotEmpty();
            RuleFor(x => x.EthnicBackground).NotEmpty();
        }
    }
}