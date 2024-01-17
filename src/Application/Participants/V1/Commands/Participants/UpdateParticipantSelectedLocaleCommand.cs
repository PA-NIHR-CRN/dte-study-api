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

public class UpdateParticipantSelectedLocaleCommand : IRequest<Response<object>>
{
    public UpdateParticipantSelectedLocaleCommand(string participantId, string selectedLocale)
    {
        ParticipantId = participantId;
        SelectedLocale = selectedLocale;
    }

    private string ParticipantId { get; }
    private string SelectedLocale { get; set; }

    public class
        UpdateParticipantSelectedLocaleCommandHandler : IRequestHandler<UpdateParticipantSelectedLocaleCommand,
            Response<object>>
    {
        private readonly IHeaderService _headerService;
        private readonly ILogger<UpdateParticipantSelectedLocaleCommandHandler> _logger;
        private readonly IParticipantRepository _participantRepository;
        private readonly IClock _clock;

        public UpdateParticipantSelectedLocaleCommandHandler(IParticipantRepository participantRepository,
            IHeaderService headerService, ILogger<UpdateParticipantSelectedLocaleCommandHandler> logger, IClock clock)
        {
            _participantRepository = participantRepository;
            _headerService = headerService;
            _logger = logger;
            _clock = clock;
        }

        public async Task<Response<object>> Handle(UpdateParticipantSelectedLocaleCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _participantRepository.GetParticipantDetailsAsync(request.ParticipantId);

                if (entity == null) throw new NotFoundException($"Participant not found, Id: {request.ParticipantId}");

                entity.SelectedLocale = request.SelectedLocale;
                entity.UpdatedAtUtc = _clock.Now();

                await _participantRepository.UpdateParticipantDetailsAsync(entity);

                return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
            }
            catch (HttpServiceException ex)
            {
                var exceptionResponse = Response<object>.CreateHttpExceptionResponse(
                    nameof(UpdateParticipantSelectedLocaleCommandHandler), ex, "err",
                    _headerService.GetConversationId());
                _logger.LogError(ex,
                    "Error updating participant details for {RequestParticipantId} - StatusCode: {ExHttpStatusCode}\\r\\n{SerializeObject}",
                    request.ParticipantId, ex.HttpStatusCode,
                    JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented));
                return exceptionResponse;
            }
            catch (Exception ex)
            {
                var exceptionResponse = Response<object>.CreateExceptionResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UpdateParticipantSelectedLocaleCommandHandler), "err",
                    ex,
                    _headerService.GetConversationId());
                _logger.LogError(ex,
                    "Unknown error updating participant details for {RequestParticipantId}\\r\\n{SerializeObject}",
                    request.ParticipantId, JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented));
                return exceptionResponse;
            }
        }
    }
}