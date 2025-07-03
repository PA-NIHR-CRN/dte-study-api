using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Application.Mappings.Participants;
using Application.Responses.V1.Participants;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Participants.V1.Queries.Participants
{
    public class GetParticipantDemographicsQuery : IRequest<Response<ParticipantDemographicsResponse>>
    {
        private string ParticipantId { get; }

        public GetParticipantDemographicsQuery(string participantId)
        {
            ParticipantId = participantId;
        }

        public class GetParticipantDemographicsQueryHandler : IRequestHandler<GetParticipantDemographicsQuery,
            Response<ParticipantDemographicsResponse>>
        {
            private readonly IParticipantRepository _participantRepository;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetParticipantDemographicsQueryHandler> _logger;

            public GetParticipantDemographicsQueryHandler(IParticipantRepository participantRepository,
                IHeaderService headerService, ILogger<GetParticipantDemographicsQueryHandler> logger)
            {
                _participantRepository = participantRepository;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<ParticipantDemographicsResponse>> Handle(GetParticipantDemographicsQuery request, CancellationToken cancellationToken)

            {
                try
                {
                    var participantDemographics =
                        await _participantRepository.GetParticipantDemographicsAsync(request.ParticipantId);

                    if (participantDemographics == null)
                    {
                        throw new NotFoundException(
                            $"No participant demographics found for participantId: {request.ParticipantId}");
                    }
                    

                    if (!participantDemographics.HasDemographics)
                    {
                        return Response<ParticipantDemographicsResponse>.CreateNotFoundResponse(
                            ProjectAssemblyNames.ApiAssemblyName, nameof(GetParticipantDemographicsQueryHandler), "err",
                            "Participant demographics not found", _headerService.GetConversationId());
                    }

                    return Response<ParticipantDemographicsResponse>.CreateSuccessfulContentResponse(
                        ParticipantMapper.MapTo(participantDemographics), _headerService.GetConversationId());

                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<ParticipantDemographicsResponse>.CreateExceptionResponse(
                        ProjectAssemblyNames.ApiAssemblyName, nameof(GetParticipantDemographicsQueryHandler), "err", ex,
                        _headerService.GetConversationId());
                    _logger.LogError(ex,
                        "Unknown error getting participant demographics for {ParticipantId}: {@exceptionResponse}", request.ParticipantId, exceptionResponse);
                    return exceptionResponse;
                }
            }
        }
    }
}