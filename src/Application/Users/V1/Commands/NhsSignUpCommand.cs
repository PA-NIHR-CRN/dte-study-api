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
        private string SelectedLanguage { get; }
        private string Token { get; }

        public NhsSignUpCommand(bool consentRegistration, string selectedLanguage, string token)
        {
            ConsentRegistration = consentRegistration;
            SelectedLanguage = selectedLanguage;
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
                return await _userService.NhsSignUpAsync(request.ConsentRegistration, request.SelectedLanguage, request.Token);
            }
        }
    }
}