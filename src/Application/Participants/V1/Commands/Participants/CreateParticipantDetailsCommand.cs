using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Dte.Common.Contracts;
using Domain.Entities.Participants;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Participants.V1.Commands.Participants
{
    public class CreateParticipantDetailsCommand : IRequest<Response<object>>
    {
        public string ParticipantId { get; }
        public string Email { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public bool ConsentRegistration { get; }
        public string NhsId { get; }
        public string NhsNumber { get; }
        public DateTime? DateOfBirth { get; set; }

        public CreateParticipantDetailsCommand(string participantId, string email, string firstname, string lastname,
            bool consentRegistration, string nhsId, DateTime? dateOfBirth, string nhsNumber)
        {
            ParticipantId = participantId;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            ConsentRegistration = consentRegistration;
            NhsId = nhsId;
            NhsNumber = nhsNumber;
            DateOfBirth = dateOfBirth;
        }

        public class CreateParticipantDetailsCommandHandler : IRequestHandler<CreateParticipantDetailsCommand, Response<object>>
        {
            private readonly IParticipantRepository _participantRepository;
            private readonly IClock _clock;
            private readonly IHeaderService _headerService;
            private readonly ILogger<CreateParticipantDetailsCommandHandler> _logger;

            public CreateParticipantDetailsCommandHandler(IParticipantRepository participantRepository, IClock clock,
                IHeaderService headerService,
                ILogger<CreateParticipantDetailsCommandHandler> logger)
            {
                _participantRepository = participantRepository;
                _clock = clock;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(CreateParticipantDetailsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = new ParticipantDetails
                    {
                        NhsId = request.NhsId,
                        NhsNumber = request.NhsNumber,
                        ParticipantId = request.ParticipantId,
                        Email = request.Email.ToLower(),
                        Firstname = request.Firstname,
                        Lastname = request.Lastname,
                        ConsentRegistration = request.ConsentRegistration,
                        DateOfBirth = request.DateOfBirth,
                        ConsentRegistrationAtUtc = request.ConsentRegistration ? _clock.Now() : (DateTime?)null,
                        RemovalOfConsentRegistrationAtUtc = (DateTime?)null,
                        CreatedAtUtc = _clock.Now(),
                    };

                    await _participantRepository.CreateParticipantDetailsAsync(entity);

                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
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