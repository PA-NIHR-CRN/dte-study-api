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
    public class GetParticipantDetailsQuery : IRequest<Response<ParticipantDetailsResponse>>
    {
        public string ParticipantId { get; set; }

        public GetParticipantDetailsQuery(string participantId)
        {
            ParticipantId = participantId;
        }
        
        public class GetParticipantDetailsQueryHandler : IRequestHandler<GetParticipantDetailsQuery, Response<ParticipantDetailsResponse>>
        {
            private readonly IParticipantApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetParticipantDetailsQueryHandler> _logger;

            public GetParticipantDetailsQueryHandler(IParticipantApiClient client, IHeaderService headerService, ILogger<GetParticipantDetailsQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }
            
            public async Task<Response<ParticipantDetailsResponse>> Handle(GetParticipantDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _client.GetParticipantDetailsAsync(request.ParticipantId);

                    return Response<ParticipantDetailsResponse>.CreateSuccessfulContentResponse(ParticipantMapper.MapTo(response), _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<ParticipantDetailsResponse>.CreateHttpExceptionResponse(nameof(GetParticipantDetailsQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting participant details for {request.ParticipantId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<ParticipantDetailsResponse>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetParticipantDetailsQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error getting participant details for {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}