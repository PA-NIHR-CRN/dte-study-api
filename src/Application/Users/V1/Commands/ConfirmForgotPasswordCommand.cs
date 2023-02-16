using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1.Users;
using Dte.Common.Responses;
using MediatR;

namespace Application.Users.V1.Commands
{
    public class ConfirmForgotPasswordCommand : IRequest<Response<ConfirmForgotPasswordResponse>>
    {
        public string Code { get; }
        public string Email { get; }
        public string Password { get; }

        public ConfirmForgotPasswordCommand(string code, string email, string password)
        {
            Code = code;
            Email = email;
            Password = password;
        }

        public class ConfirmForgotPasswordCommandHandler : IRequestHandler<ConfirmForgotPasswordCommand, Response<ConfirmForgotPasswordResponse>>
        {
            private readonly IUserService _userService;

            public ConfirmForgotPasswordCommandHandler(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<Response<ConfirmForgotPasswordResponse>> Handle(ConfirmForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                return await _userService.ConfirmForgotPasswordAsync(request.Code, request.Email, request.Password);
            }
        }
    }
}