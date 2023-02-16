using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Application.Responses.V1.Users;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.V1.Commands
{
    public class UserLoginCommand : IRequest<Response<UserLoginResponse>>
    {
        public string Email { get; }
        public string Password { get; }

        public UserLoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Response<UserLoginResponse>>
        {
            private readonly IFeatureFlagService _featureFlagService;
            private readonly IUserService _userService;
            private readonly IAccessWhitelistRepository _accessWhitelistRepository;
            private readonly IHeaderService _headerService;
            private readonly ILogger<UserLoginCommandHandler> _logger;

            public UserLoginCommandHandler(IFeatureFlagService featureFlagService,
                IUserService userService,
                IAccessWhitelistRepository accessWhitelistRepository,
                IHeaderService headerService,
                ILogger<UserLoginCommandHandler> logger)
            {
                _featureFlagService = featureFlagService;
                _userService = userService;
                _accessWhitelistRepository = accessWhitelistRepository;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<UserLoginResponse>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
            {
                var privateBetaEmailWhitelistFeatureFlag = await _featureFlagService.GetPrivateBetaEmailWhitelistFeatureFlag();

                if (privateBetaEmailWhitelistFeatureFlag != null && privateBetaEmailWhitelistFeatureFlag.Enabled)
                {
                    var whitelist = await _accessWhitelistRepository.GetWhitelistByEmail(request.Email);

                    if (whitelist == null)
                    {
                        _logger.LogWarning("Attempted to login user but they are not whitelisted");
                        return Response<UserLoginResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(UserLoginCommandHandler), "User_Not_In_Allow_List_Error", "User is not in the allow list", _headerService.GetConversationId());
                    }
                }
                
                return await _userService.LoginAsync(request.Email, request.Password);
            }
        }
    }
}