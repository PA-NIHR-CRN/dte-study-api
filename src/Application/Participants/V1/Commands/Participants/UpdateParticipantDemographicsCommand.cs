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
    public class UpdateParticipantDemographicsCommand : IRequest<Response<object>>
    {
        public string ParticipantId { get; set; }
        public string MobileNumber { get; set; }
        public string LandlineNumber { get; set; }
        public ParticipantAddressModel Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SexRegisteredAtBirth { get; set; }
        public bool? GenderIsSameAsSexRegisteredAtBirth { get; set; }
        public string EthnicGroup { get; set; }
        public string EthnicBackground { get; set; }
        public bool? Disability { get; set; }
        public string DisabilityDescription { get; set; }
        public IEnumerable<string> HealthConditionInterests { get; set; }

        public UpdateParticipantDemographicsCommand(string participantId,
            string mobileNumber,
            string landlineNumber,
            ParticipantAddressModel address,
            DateTime dateOfBirth,
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
            DateOfBirth = dateOfBirth;
            SexRegisteredAtBirth = sexRegisteredAtBirth;
            GenderIsSameAsSexRegisteredAtBirth = genderIsSameAsSexRegisteredAtBirth;
            EthnicGroup = ethnicGroup;
            EthnicBackground = ethnicBackground;
            Disability = disability;
            DisabilityDescription = disabilityDescription;
            HealthConditionInterests = healthConditionInterests;
        }

        public class UpdateParticipantDemographicsCommandHandler : IRequestHandler<UpdateParticipantDemographicsCommand, Response<object>>
        {
            private readonly IParticipantApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<UpdateParticipantDemographicsCommandHandler> _logger;
            
            public UpdateParticipantDemographicsCommandHandler(IParticipantApiClient client, IHeaderService headerService, ILogger<UpdateParticipantDemographicsCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(UpdateParticipantDemographicsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.UpdateParticipantDemographicsAsync(request.ParticipantId, ParticipantMapper.MapTo(request));

                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(UpdateParticipantDemographicsCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error updating participant demographics for {request.ParticipantId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(UpdateParticipantDemographicsCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error updating participant demographics for {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}