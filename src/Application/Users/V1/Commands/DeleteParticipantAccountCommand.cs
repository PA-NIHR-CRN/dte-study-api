using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Participant.Api.Client;
using Dte.Participant.Api.Client.Requests.Participants;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.V1.Commands
{
    public class DeleteParticipantAccountCommand : IRequest<Response<object>>
    {
        private string UserName { get; }
        private string ParticipantId { get; }

        public DeleteParticipantAccountCommand(string userName, string participantId)
        {
            UserName = userName;
            ParticipantId = participantId;
        }

        public class DeleteParticipantAccountCommandHandler : IRequestHandler<DeleteParticipantAccountCommand, Response<object>>
        {
            private readonly IUserService _userService;
            private readonly IParticipantApiClient _participantApiClient;
            private readonly IHeaderService _headerService;
            private readonly ILogger<DeleteParticipantAccountCommandHandler> _logger;

            public DeleteParticipantAccountCommandHandler(IUserService userService,
                IHeaderService headerService,
                IParticipantApiClient participantApiClient,
                ILogger<DeleteParticipantAccountCommandHandler> logger)
            {
                _userService = userService;
                _headerService = headerService;
                _participantApiClient = participantApiClient;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(DeleteParticipantAccountCommand request, CancellationToken cancellationToken)
            {                
                // clear out personal details
                await _participantApiClient.DeleteParticipantAccountAsync(new DeleteParticipantAccountRequest
                {
                    ParticipantId = request.ParticipantId
                });

                return await _userService.DeleteUserAsync(request.ParticipantId);
            }
        }
    }
}