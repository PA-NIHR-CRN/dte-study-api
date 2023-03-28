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
    public class CreateParticipantDetailsCommand : IRequest<Response<object>>
    {
        public string ParticipantId { get; set; }
        public string Email { get; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool ConsentRegistration { get; set; }
        public string NhsId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NhsNumber { get; set; }

        public CreateParticipantDetailsCommand(string participantId, string email, string firstname, string lastname,
            bool consentRegistration, string nhsId, DateTime dateOfBirth, string nhsNumber)
        {
            ParticipantId = participantId;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            ConsentRegistration = consentRegistration;
            NhsId = nhsId;
            DateOfBirth = dateOfBirth;
            NhsNumber = nhsNumber;
        }

        public class
            CreateParticipantDetailsCommandHandler : IRequestHandler<CreateParticipantDetailsCommand, Response<object>>
        {
            private readonly IParticipantApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<CreateParticipantDetailsCommandHandler> _logger;

            public CreateParticipantDetailsCommandHandler(IParticipantApiClient client, IHeaderService headerService,
                ILogger<CreateParticipantDetailsCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(CreateParticipantDetailsCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    await _client.CreateParticipantDetailsAsync(ParticipantMapper.MapTo(request));

                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(
                        nameof(CreateParticipantDetailsCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex,
                        $"Error creating participant details for {request.ParticipantId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(
                        ProjectAssemblyNames.ApiAssemblyName, nameof(CreateParticipantDetailsCommandHandler), "err", ex,
                        _headerService.GetConversationId());
                    _logger.LogError(ex,
                        $"Unknown error creating participant details for {request.ParticipantId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}