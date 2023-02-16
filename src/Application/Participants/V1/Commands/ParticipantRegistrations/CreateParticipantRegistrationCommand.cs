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

namespace Application.Participants.V1.Commands.ParticipantRegistrations
{
    public class CreateParticipantRegistrationCommand : IRequest<Response<object>>
    {
        public long StudyId { get; }
        public string SiteId { get; }
        public string ParticipantId { get; }

        public CreateParticipantRegistrationCommand(long studyId, string siteId, string participantId)
        {
            StudyId = studyId;
            SiteId = siteId;
            ParticipantId = participantId;
        }
        
        public class CreateParticipantRegistrationCommandHandler : IRequestHandler<CreateParticipantRegistrationCommand, Response<object>>
        {
            private readonly IParticipantApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<CreateParticipantRegistrationCommandHandler> _logger;

            public CreateParticipantRegistrationCommandHandler(IParticipantApiClient client, IHeaderService headerService, ILogger<CreateParticipantRegistrationCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }
            
            public async Task<Response<object>> Handle(CreateParticipantRegistrationCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.CreateParticipantRegistrationAsync(ParticipantMapper.MapTo(request));

                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(CreateParticipantRegistrationCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error creating participant registration for {request.ParticipantId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(CreateParticipantRegistrationCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error creating participant registration for {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}