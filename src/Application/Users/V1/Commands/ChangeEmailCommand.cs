using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.V1.Commands
{
    public class ChangeEmailCommand : IRequest<Response<object>>
    {
        public string ParticipantId { get; }
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }

        public ChangeEmailCommand(string participantId, string currentEmail, string newEmail)
        {
            ParticipantId = participantId;
            CurrentEmail = currentEmail;
            NewEmail = newEmail;
        }

        public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, Response<object>>
        {
            private readonly IUserService _userService;
            private readonly IParticipantService _participantService;
            private readonly ILogger<ChangeEmailCommandHandler> _logger;

            public ChangeEmailCommandHandler(IUserService userService, IParticipantService participantService, ILogger<ChangeEmailCommandHandler> logger)
            {
                _userService = userService;
                _participantService = participantService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
            {
                var clientResponse =  await _userService.ChangeEmailAsync(request.CurrentEmail, request.NewEmail);

                if (clientResponse != null && clientResponse.IsSuccess)
                {
                    await _participantService.UpdateParticipantEmail(request.ParticipantId,
                    
                        request.NewEmail);
                }

                return clientResponse;
            }
        }
    }
}