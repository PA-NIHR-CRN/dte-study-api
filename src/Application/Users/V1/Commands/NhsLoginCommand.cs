using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1.Users;
using Dte.Common.Responses;
using MediatR;

namespace Application.Users.V1.Commands
{
    public class NhsLoginCommand : IRequest<Response<NhsLoginResponse>>
    {
        private string Code { get; }
        private string RedirectUrl { get; }

        public NhsLoginCommand(string code, string redirectUrl)
        {
            Code = code;
            RedirectUrl = redirectUrl;
        }

        public class NhsLoginCommandHandler : IRequestHandler<NhsLoginCommand, Response<NhsLoginResponse>>
        {
            private readonly INhsLoginService _nhsLoginService;

            public NhsLoginCommandHandler(
                INhsLoginService nhsService)
            {
                _nhsLoginService = nhsService;
            }

            public async Task<Response<NhsLoginResponse>> Handle(NhsLoginCommand request,
                CancellationToken cancellationToken)
            {
                return await _nhsLoginService.NhsLoginAsync(request.Code, request.RedirectUrl);
            }
        }
    }
}
