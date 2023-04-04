using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.Participants;
using Application.Models.Participants;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Participant.Api.Client;
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
        public IEnumerable<string> HealthConditionInterests { get; set; }

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

        public class CreateParticipantDemographicsCommandHandler : IRequestHandler<CreateParticipantDemographicsCommand, Response<object>>
        {
            private readonly IParticipantApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<CreateParticipantDemographicsCommandHandler> _logger;

            public CreateParticipantDemographicsCommandHandler(IParticipantApiClient client, IHeaderService headerService, ILogger<CreateParticipantDemographicsCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }
            
            public async Task<Response<object>> Handle(CreateParticipantDemographicsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.CreateParticipantDemographicsAsync(ParticipantMapper.MapTo(request));

                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(UpdateParticipantDetailsCommand.UpdateParticipantDetailsCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error creating participant demographics for {request.ParticipantId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(UpdateParticipantDetailsCommand.UpdateParticipantDetailsCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error creating participant demographics for {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}