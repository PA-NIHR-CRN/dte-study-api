using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Dte.Common.Responses;
using Dte.Participant.Api.Client;
using Dte.Participant.Api.Client.Requests.Participants;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.V1.Commands
{
    public class ChangeEmailCommand : IRequest<Response<object>>
    {
        public string ParticipantId { get; }
        public string AccessToken { get; set; }
        public string NewEmail { get; set; }

        public ChangeEmailCommand(string participantId, string accessToken, string newEmail)
        {
            ParticipantId = participantId;
            AccessToken = accessToken;
            NewEmail = newEmail;
        }

        public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, Response<object>>
        {
            private readonly IUserService _userService;
            private readonly IParticipantApiClient _participantApiClient;
            private readonly ILogger<ChangeEmailCommandHandler> _logger;

            public ChangeEmailCommandHandler(IUserService userService, IParticipantApiClient participantApiClient, ILogger<ChangeEmailCommandHandler> logger)
            {
                _userService = userService;
                _participantApiClient = participantApiClient;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
            {
                var clientResponse =  await _userService.ChangeEmailAsync(request.AccessToken, request.NewEmail);

                if (clientResponse != null && clientResponse.IsSuccess)
                {
                    await _participantApiClient.UpdateParticipantEmailAsync(request.ParticipantId, new UpdateParticipantEmailRequest
                    {
                        Email = request.NewEmail
                    });
                }

                return clientResponse;
            }
        }
    }
}