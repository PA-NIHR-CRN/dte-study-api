using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1.Users;
using MediatR;

namespace Application.Users.V1.Queries
{
    public class GetPasswordPolicyQuery : IRequest<PasswordPolicyTypeResponse>
    {
        public class GetPasswordPolicyQueryHandler : IRequestHandler<GetPasswordPolicyQuery, PasswordPolicyTypeResponse>
        {
            private readonly IUserService _userService;

            public GetPasswordPolicyQueryHandler(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<PasswordPolicyTypeResponse> Handle(GetPasswordPolicyQuery request, CancellationToken cancellationToken)
            {
                return await _userService.GetPasswordPolicyTypeAsync();
            }
        }
    }
}