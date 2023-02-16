using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.Participants;
using Application.Responses.V1.Participants;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Participant.Api.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Participants.V1.Queries.Participants
{
    public class GetParticipantSuitabilityQuery : IRequest<Response<ParticipantSuitabilityResponse>>
    {
        public string ParticipantId { get; }
        public long StudyId { get; }

        public GetParticipantSuitabilityQuery(string participantId, long studyId)
        {
            ParticipantId = participantId;
            StudyId = studyId;
        }

        public class GetParticipantSuitabilityQueryHandler : IRequestHandler<GetParticipantSuitabilityQuery, Response<ParticipantSuitabilityResponse>>
        {
            private readonly IParticipantApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetParticipantSuitabilityQueryHandler> _logger;

            public GetParticipantSuitabilityQueryHandler(IParticipantApiClient client, IHeaderService headerService, ILogger<GetParticipantSuitabilityQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<ParticipantSuitabilityResponse>> Handle(GetParticipantSuitabilityQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _client.GetParticipantSuitabilityAsync(request.StudyId, request.ParticipantId);

                    return Response<ParticipantSuitabilityResponse>.CreateSuccessfulContentResponse(ParticipantMapper.MapTo(response), _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<ParticipantSuitabilityResponse>.CreateHttpExceptionResponse(nameof(GetParticipantSuitabilityQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting participant suitability for Study: {request.StudyId} - Participant: {request.ParticipantId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<ParticipantSuitabilityResponse>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetParticipantSuitabilityQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error getting participant suitability for Study: {request.StudyId} - Participant: {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}