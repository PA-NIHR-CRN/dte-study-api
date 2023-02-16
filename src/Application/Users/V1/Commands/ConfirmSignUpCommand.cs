using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Dte.Common.Responses;
using MediatR;

namespace Application.Users.V1.Commands
{
    public class ConfirmSignUpCommand : IRequest<Response<object>>
    {
        public string Code { get; }
        public string Email { get; }

        public ConfirmSignUpCommand(string code, string email)
        {
            Code = code;
            Email = email;
        }

        public class ConfirmSignUpCommandHandler : IRequestHandler<ConfirmSignUpCommand, Response<object>>
        {
            private readonly IUserService _userService;

            public ConfirmSignUpCommandHandler(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<Response<object>> Handle(ConfirmSignUpCommand request, CancellationToken cancellationToken)
            {
                return await _userService.ConfirmSignUpAsync(request.Code, request.Email);
            }
        }
    }
}