using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Application.Mappings.Participants;
using Application.Models.Participants;
using Domain.Entities.Participants;
using Dte.Common.Contracts;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Participants.V1.Commands.Participants
{
    public class CreateParticipantDemographicsCommand : IRequest<Response<object>>
    {
        public string ParticipantId { get; }
        public string MobileNumber { get; }
        public string LandlineNumber { get; }
        public ParticipantAddressModel Address { get; }
        public string SexRegisteredAtBirth { get; }
        public bool? GenderIsSameAsSexRegisteredAtBirth { get; }
        public string EthnicGroup { get; }
        public string EthnicBackground { get; }
        public bool? Disability { get; }
        public string DisabilityDescription { get; }
        public IEnumerable<string> HealthConditionInterests { get; }

        public CreateParticipantDemographicsCommand(string participantId,
            string mobileNumber,
            string landlineNumber,
            ParticipantAddressModel address,
            string sexRegisteredAtBirth,
            bool? genderIsSameAsSexRegisteredAtBirth,
            string ethnicGroup,
            string ethnicBackground,
            bool? disability,
            string disabilityDescription,
            IEnumerable<string> healthConditionInterests)
        {
            ParticipantId = participantId;
            MobileNumber = mobileNumber;
            LandlineNumber = landlineNumber;
            Address = address;
            SexRegisteredAtBirth = sexRegisteredAtBirth;
            GenderIsSameAsSexRegisteredAtBirth = genderIsSameAsSexRegisteredAtBirth;
            EthnicGroup = ethnicGroup;
            EthnicBackground = ethnicBackground;
            Disability = disability;
            DisabilityDescription = disabilityDescription;
            HealthConditionInterests = healthConditionInterests;
        }

        public class
            CreateParticipantDemographicsCommandHandler : IRequestHandler<CreateParticipantDemographicsCommand,
                Response<object>>
        {
            private readonly IParticipantRepository _participantRepository;
            private readonly IClock _clock;
            private readonly IHeaderService _headerService;
            private readonly ILogger<CreateParticipantDemographicsCommandHandler> _logger;

            public CreateParticipantDemographicsCommandHandler(IParticipantRepository participantRepository,
                IClock clock, IHeaderService headerService, ILogger<CreateParticipantDemographicsCommandHandler> logger)
            {
                _participantRepository = participantRepository;
                _clock = clock;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(CreateParticipantDemographicsCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var updateExisting = false;

                    var entity = await _participantRepository.GetParticipantDemographicsAsync(request.ParticipantId);

                    if (entity == null)
                    {
                        entity = new ParticipantDemographics
                        {
                            ParticipantId = request.ParticipantId,
                        };
                        updateExisting = false;
                    }
                    else
                    {
                        updateExisting = true;
                    }

                    entity.MobileNumber = request.MobileNumber;
                    entity.LandlineNumber = request.LandlineNumber;
                    entity.SexRegisteredAtBirth = request.SexRegisteredAtBirth;
                    entity.GenderIsSameAsSexRegisteredAtBirth = request.GenderIsSameAsSexRegisteredAtBirth;
                    entity.EthnicGroup = request.EthnicGroup;
                    entity.EthnicBackground = request.EthnicBackground;
                    entity.Disability = request.Disability;
                    entity.DisabilityDescription = request.DisabilityDescription;
                    entity.HealthConditionInterests = request.HealthConditionInterests?.ToList();

                    if (request.Address != null)
                    {
                        entity.Address = ParticipantAddressMapper.MapTo(request.Address);
                    }

                    if (updateExisting)
                    {
                        await _participantRepository.UpdateParticipantDemographicsAsync(entity);
                    }
                    else
                    {
                        await _participantRepository.CreateParticipantDemographicsAsync(entity);
                    }

                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(
                        ProjectAssemblyNames.ApiAssemblyName,
                        nameof(UpdateParticipantDetailsCommand.UpdateParticipantDetailsCommandHandler), "err", ex,
                        _headerService.GetConversationId());
                    _logger.LogError(ex,
                        $"Unknown error creating participant demographics for {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}