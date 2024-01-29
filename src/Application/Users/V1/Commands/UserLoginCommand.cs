using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Dte.Common.Responses;
using MediatR;

namespace Application.Users.V1.Commands;

public class UserLoginCommand : IRequest<Response<string>>
{
    public UserLoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    private string Email { get; }
    private string Password { get; }

    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Response<string>>
    {
        private readonly IUserService _userService;

        public UserLoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Response<string>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            return await _userService.LoginAsync(request.Email, request.Password);
        }
    }
}
