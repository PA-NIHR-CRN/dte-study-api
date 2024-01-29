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
        private string Token { get; }

        public NhsSignUpCommand(bool consentRegistration, string token)
        {
            ConsentRegistration = consentRegistration;
            Token = token;
        }

        public class CreateUserCommandHandler : IRequestHandler<NhsSignUpCommand, Response<SignUpResponse>>
        {
            private readonly INhsLoginService _nhsLoginService;

            public CreateUserCommandHandler(
                INhsLoginService nhsLoginService)
            {
                _nhsLoginService = nhsLoginService;
            }

            public async Task<Response<SignUpResponse>> Handle(NhsSignUpCommand request,
                CancellationToken cancellationToken)
            {
                return await _nhsLoginService.NhsSignUpAsync(request.ConsentRegistration, request.Token);
            }
        }
    }
}
