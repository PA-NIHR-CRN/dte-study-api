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
    public class GetParticipantDetailsQuery : IRequest<Response<ParticipantDetailsResponse>>
    {
        private string ParticipantId { get; }

        public GetParticipantDetailsQuery(string participantId)
        {
            ParticipantId = participantId;
        }

        public class
            GetParticipantDetailsQueryHandler : IRequestHandler<GetParticipantDetailsQuery,
                Response<ParticipantDetailsResponse>>
        {
            private readonly IParticipantRepository _participantRepository;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetParticipantDetailsQueryHandler> _logger;

            public GetParticipantDetailsQueryHandler(IParticipantRepository participantRepository,
                IHeaderService headerService, ILogger<GetParticipantDetailsQueryHandler> logger)
            {
                _participantRepository = participantRepository;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<ParticipantDetailsResponse>> Handle(GetParticipantDetailsQuery request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var participantDetails =
                        await _participantRepository.GetParticipantDetailsAsync(request.ParticipantId);

                    if (participantDetails == null)
                    {
                        throw new NotFoundException(
                            $"No participant details found for participantId: {request.ParticipantId}");
                    }

                    return Response<ParticipantDetailsResponse>.CreateSuccessfulContentResponse(
                        ParticipantMapper.MapTo(participantDetails), _headerService.GetConversationId());
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<ParticipantDetailsResponse>.CreateExceptionResponse(
                        ProjectAssemblyNames.ApiAssemblyName, nameof(GetParticipantDetailsQueryHandler), "err", ex,
                        _headerService.GetConversationId());
                    _logger.LogError(ex,
                        "Unknown error getting participant details for {ParticipantId}: {@exceptionResponse}", request.ParticipantId, exceptionResponse);
                    return exceptionResponse;
                }
            }
        }
    }
}