using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Application.Mappings.Participants;
using Application.Models.Participants;
using Dte.Common.Contracts;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Participants.V1.Commands.Participants
{
    public class UpdateParticipantDemographicsCommand : IRequest<Response<object>>
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
        public DateTime? DateOfBirth { get; set; }

        public UpdateParticipantDemographicsCommand(string participantId,
            string mobileNumber,
            string landlineNumber,
            ParticipantAddressModel address,
            string sexRegisteredAtBirth,
            bool? genderIsSameAsSexRegisteredAtBirth,
            string ethnicGroup,
            string ethnicBackground,
            bool? disability,
            string disabilityDescription,
            IEnumerable<string> healthConditionInterests,
            DateTime? dateOfBirth
        )
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
            DateOfBirth = dateOfBirth;
        }

        public class
            UpdateParticipantDemographicsCommandHandler : IRequestHandler<UpdateParticipantDemographicsCommand,
                Response<object>>
        {
            private readonly IParticipantRepository _participantRepository;
            private readonly IClock _clock;
            private readonly IHeaderService _headerService;
            private readonly ILogger<UpdateParticipantDemographicsCommandHandler> _logger;

            public UpdateParticipantDemographicsCommandHandler(IParticipantRepository participantRepository,
                IClock clock, IHeaderService headerService, ILogger<UpdateParticipantDemographicsCommandHandler> logger)
            {
                _participantRepository = participantRepository;
                _clock = clock;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(UpdateParticipantDemographicsCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await _participantRepository.GetParticipantDemographicsAsync(request.ParticipantId);

                    entity.MobileNumber = request.MobileNumber;
                    entity.LandlineNumber = request.LandlineNumber;
                    entity.SexRegisteredAtBirth = request.SexRegisteredAtBirth;
                    entity.GenderIsSameAsSexRegisteredAtBirth = request.GenderIsSameAsSexRegisteredAtBirth;
                    entity.EthnicGroup = request.EthnicGroup;
                    entity.EthnicBackground = request.EthnicBackground;
                    entity.Disability = request.Disability;
                    entity.DisabilityDescription = request.DisabilityDescription;
                    entity.HealthConditionInterests = request.HealthConditionInterests?.ToList();
                    entity.DateOfBirth = request.DateOfBirth;
                    entity.UpdatedAtUtc = _clock.Now();

                    if (request.Address != null)
                    {
                        entity.Address = ParticipantAddressMapper.MapTo(request.Address);
                    }

                    await _participantRepository.UpdateParticipantDemographicsAsync(entity);

                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(
                        nameof(UpdateParticipantDemographicsCommandHandler), ex, "err",
                        _headerService.GetConversationId());
                    _logger.LogError(ex,
                        $"Error updating participant demographics for {request.ParticipantId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(
                        ProjectAssemblyNames.ApiAssemblyName, nameof(UpdateParticipantDemographicsCommandHandler),
                        "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex,
                        $"Unknown error updating participant demographics for {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}