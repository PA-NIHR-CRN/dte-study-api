using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1.Users;
using Dte.Common.Responses;
using MediatR;

namespace Application.Users.V1.Commands;

public class SignUpCommand : IRequest<Response<SignUpResponse>>
{
    public SignUpCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    private string Email { get; }
    private string Password { get; }

    public class CreateUserCommandHandler : IRequestHandler<SignUpCommand, Response<SignUpResponse>>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Response<SignUpResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            return await _userService.SignUpAsync(request.Email, request.Password);
        }
    }
}
