using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1.Users;
using Dte.Common.Responses;
using MediatR;

namespace Application.Users.V1.Commands
{
    public class ResendVerificationEmailCommand : IRequest<Response<ResendConfirmationCodeResponse>>
    {
        private string UserId { get; }

        public ResendVerificationEmailCommand(string userId)
        {
            UserId = userId;
        }

        public class ResendVerificationEmailHandler : IRequestHandler<ResendVerificationEmailCommand, Response<ResendConfirmationCodeResponse>>
        {
            private readonly IUserService _userService;

            public ResendVerificationEmailHandler(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<Response<ResendConfirmationCodeResponse>> Handle(ResendVerificationEmailCommand request, CancellationToken cancellationToken)
            {
                return await _userService.ResendVerificationEmailAsync(request.UserId);
            }
        }
    }
}