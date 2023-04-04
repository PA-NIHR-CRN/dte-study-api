using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.V1.Commands
{
    public class ChangePasswordCommand : IRequest<Response<object>>
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public ChangePasswordCommand(string email, string oldPassword, string newPassword)
        {
            Email = email;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response<object>>
        {
            private readonly IUserService _userService;
            private readonly IHeaderService _headerService;
            private readonly ILogger<ChangePasswordCommandHandler> _logger;

            public ChangePasswordCommandHandler(IUserService userService,
                IHeaderService headerService,
                ILogger<ChangePasswordCommandHandler> logger)
            {
                _userService = userService;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                return await  _userService.ChangePasswordAsync(request.Email, request.OldPassword, request.NewPassword);
            }
        }
    }
}