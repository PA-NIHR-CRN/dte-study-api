using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1.Users;
using Dte.Common.Responses;
using MediatR;

namespace Application.Users.V1.Commands;

public class SignUpCommand : IRequest<Response<SignUpResponse>>
{
    public SignUpCommand(string email, string password, string selectedLocale)
    {
        Email = email;
        Password = password;
        SelectedLocale = selectedLocale;
    }

    private string Email { get; }
    private string Password { get; }
    private string SelectedLocale { get; }

    public class CreateUserCommandHandler : IRequestHandler<SignUpCommand, Response<SignUpResponse>>
    {
        private readonly IAuthenticationService _authenticationService;

        public CreateUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Response<SignUpResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationService.SignUpAsync(request.Email, request.Password, request.SelectedLocale);
        }
    }
}
