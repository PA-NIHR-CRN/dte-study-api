using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1.Users;
using Dte.Common.Responses;
using MediatR;

namespace Application.Users.V1.Commands
{
    public class NhsSignUpCommand : IRequest<Response<SignUpResponse>>
    {
        private bool ConsentRegistration { get; }
        private CultureInfo SelectedLocale { get; }
        private string Token { get; }

        public NhsSignUpCommand(bool consentRegistration, CultureInfo selectedLocale, string token)
        {
            ConsentRegistration = consentRegistration;
            SelectedLocale = selectedLocale;
            Token = token;
        }

        public class CreateUserCommandHandler : IRequestHandler<NhsSignUpCommand, Response<SignUpResponse>>
        {
            private readonly IUserService _userService;

            public CreateUserCommandHandler(
                IUserService userService)
            {
                _userService = userService;
            }

            public async Task<Response<SignUpResponse>> Handle(NhsSignUpCommand request,
                CancellationToken cancellationToken)
            {
                return await _userService.NhsSignUpAsync(request.ConsentRegistration, request.SelectedLocale, request.Token);
            }
        }
    }
}