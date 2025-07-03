using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Dte.Common.Contracts;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Participants.V1.Commands.Participants;

public class UpdateParticipantDetailsCommand : IRequest<Response<object>>
{
    public UpdateParticipantDetailsCommand(string participantId, string firstname, string lastname)
    {
        ParticipantId = participantId;
        Firstname = firstname;
        Lastname = lastname;
    }

    private string ParticipantId { get; }
    private string Firstname { get; set; }
    private string Lastname { get; set; }

    public class
        UpdateParticipantDetailsCommandHandler : IRequestHandler<UpdateParticipantDetailsCommand, Response<object>>
    {
        private readonly IHeaderService _headerService;
        private readonly ILogger<UpdateParticipantDetailsCommandHandler> _logger;
        private readonly IParticipantRepository _participantRepository;
        private readonly IClock _clock;

        public UpdateParticipantDetailsCommandHandler(IParticipantRepository participantRepository,
            IHeaderService headerService, ILogger<UpdateParticipantDetailsCommandHandler> logger, IClock clock)
        {
            _participantRepository = participantRepository;
            _headerService = headerService;
            _logger = logger;
            _clock = clock;
        }

        public async Task<Response<object>> Handle(UpdateParticipantDetailsCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _participantRepository.GetParticipantDetailsAsync(request.ParticipantId);

                if (entity == null)
                {
                    throw new NotFoundException($"Participant not found, Id: {request.ParticipantId}");
                }

                entity.Firstname = request.Firstname;
                entity.Lastname = request.Lastname;
                entity.UpdatedAtUtc = _clock.Now();

                await _participantRepository.UpdateParticipantDetailsAsync(entity);

                return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
            }
            catch (HttpServiceException ex)
            {
                var exceptionResponse = Response<object>.CreateHttpExceptionResponse(
                    nameof(UpdateParticipantDetailsCommandHandler), ex, "err", _headerService.GetConversationId());
                _logger.LogError(ex,
                    "Error updating participant details for {RequestParticipantId} - StatusCode: {ExHttpStatusCode}: {@exceptionResponse}",
                    request.ParticipantId, ex.HttpStatusCode,
                    exceptionResponse);
                return exceptionResponse;
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<object>.CreateExceptionResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UpdateParticipantDetailsCommandHandler), "err", ex,
                    _headerService.GetConversationId());
                _logger.LogError(ex,
                    "Unknown error updating participant details for {RequestParticipantId}: {@exceptionResponse}",
                    request.ParticipantId, exceptionResponse);
                return exceptionResponse;
            }
        }
    }
}