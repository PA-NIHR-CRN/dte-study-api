using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1.Users;
using Dte.Common.Responses;
using MediatR;

namespace Application.Users.V1.Commands
{
    public class ForgotPasswordCommand : IRequest<Response<ForgotPasswordResponse>>
    {
        public string Email { get; }

        public ForgotPasswordCommand(string email)
        {
            Email = email;
        }

        public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Response<ForgotPasswordResponse>>
        {
            private readonly IUserService _userService;

            public ForgotPasswordCommandHandler(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<Response<ForgotPasswordResponse>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                return await _userService.ForgotPasswordAsync(request.Email);
            }
        }
    }
}