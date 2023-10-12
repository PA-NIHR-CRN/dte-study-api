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
        private string SelectedLocale { get; }

        public NhsLoginCommand(string code, string redirectUrl, string selectedLocale)
        {
            Code = code;
            RedirectUrl = redirectUrl;
            SelectedLocale = selectedLocale;
        }

        public class NhsLoginCommandHandler : IRequestHandler<NhsLoginCommand, Response<NhsLoginResponse>>
        {
            private readonly IUserService _userService;

            public NhsLoginCommandHandler(
                IUserService userService)
            {
                _userService = userService;
            }

            public async Task<Response<NhsLoginResponse>> Handle(NhsLoginCommand request,
                CancellationToken cancellationToken)
            {
                return await _userService.NhsLoginAsync(request.Code, request.RedirectUrl, request.SelectedLocale);
            }
        }
    }
}