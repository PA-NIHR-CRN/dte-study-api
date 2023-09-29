using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Dte.Common.Http;
using Dte.Common.Responses;
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
            private readonly IParticipantService _participantService;
            private readonly IHeaderService _headerService;
            private readonly ILogger<DeleteParticipantAccountCommandHandler> _logger;

            public DeleteParticipantAccountCommandHandler(
                IHeaderService headerService,
                IParticipantService participantService,
                ILogger<DeleteParticipantAccountCommandHandler> logger)
            {
                _headerService = headerService;
                _participantService = participantService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(DeleteParticipantAccountCommand request, CancellationToken cancellationToken)
            {                
                // clear out personal details
                await _participantService.DeleteUserAsync(request.ParticipantId, cancellationToken);
                
                return Response<object>.CreateSuccessfulResponse();
            }
        }
    }
}