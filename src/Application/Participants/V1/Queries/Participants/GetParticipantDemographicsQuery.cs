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
    public class GetParticipantDemographicsQuery : IRequest<Response<ParticipantDemographicsResponse>>
    {
        public string ParticipantId { get; set; }

        public GetParticipantDemographicsQuery(string participantId)
        {
            ParticipantId = participantId;
        }
        
        public class GetParticipantDemographicsQueryHandler : IRequestHandler<GetParticipantDemographicsQuery, Response<ParticipantDemographicsResponse>>
        {
            private readonly IParticipantApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetParticipantDemographicsQueryHandler> _logger;

            public GetParticipantDemographicsQueryHandler(IParticipantApiClient client, IHeaderService headerService, ILogger<GetParticipantDemographicsQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }
            
            public async Task<Response<ParticipantDemographicsResponse>> Handle(GetParticipantDemographicsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _client.GetParticipantDemographicsAsync(request.ParticipantId);

                    if (response == null || !response.HasDemographics)
                    {
                        return Response<ParticipantDemographicsResponse>.CreateNotFoundResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetParticipantDemographicsQueryHandler), "err", "Participant demographics not found", _headerService.GetConversationId());
                    }

                    return Response<ParticipantDemographicsResponse>.CreateSuccessfulContentResponse(ParticipantMapper.MapTo(response), _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<ParticipantDemographicsResponse>.CreateHttpExceptionResponse(nameof(GetParticipantDemographicsQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting participant demographics for {request.ParticipantId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<ParticipantDemographicsResponse>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetParticipantDemographicsQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error getting participant demographics for {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}