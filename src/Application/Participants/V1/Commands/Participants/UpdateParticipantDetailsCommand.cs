using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.Participants;
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
    public class UpdateParticipantDetailsCommand : IRequest<Response<object>>
    {
        public string ParticipantId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool ConsentRegistration { get; set; }

        public UpdateParticipantDetailsCommand(string participantId, string firstname, string lastname, bool consentRegistration)
        {
            ParticipantId = participantId;
            Firstname = firstname;
            Lastname = lastname;
            ConsentRegistration = consentRegistration;
        }
        
        public class UpdateParticipantDetailsCommandHandler : IRequestHandler<UpdateParticipantDetailsCommand, Response<object>>
        {
            private readonly IParticipantApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<UpdateParticipantDetailsCommandHandler> _logger;

            public UpdateParticipantDetailsCommandHandler(IParticipantApiClient client, IHeaderService headerService, ILogger<UpdateParticipantDetailsCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }
            
            public async Task<Response<object>> Handle(UpdateParticipantDetailsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.UpdateParticipantDetailsAsync(request.ParticipantId, ParticipantMapper.MapTo(request));

                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(UpdateParticipantDetailsCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error updating participant details for {request.ParticipantId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(UpdateParticipantDetailsCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error updating participant details for {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}