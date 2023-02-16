using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1.Users;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.V1.Queries
{
    public class GetAccessWhiteListQuery : IRequest<Response<IEnumerable<AccessWhiteListResponse>>>
    {
        public class GetAccessWhiteListQueryHandler : IRequestHandler<GetAccessWhiteListQuery, Response<IEnumerable<AccessWhiteListResponse>>>
        {
            private readonly IAccessWhitelistRepository _accessWhitelistRepository;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetAccessWhiteListQueryHandler> _logger;

            public GetAccessWhiteListQueryHandler(IAccessWhitelistRepository accessWhitelistRepository, IHeaderService headerService, ILogger<GetAccessWhiteListQueryHandler> logger)
            {
                _accessWhitelistRepository = accessWhitelistRepository;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<IEnumerable<AccessWhiteListResponse>>> Handle(GetAccessWhiteListQuery request, CancellationToken cancellationToken)
            {
                var accessWhitelists = await _accessWhitelistRepository.GetWhitelist();
                return Response<IEnumerable<AccessWhiteListResponse>>.CreateSuccessfulContentResponse(accessWhitelists.Select(x => new AccessWhiteListResponse { Email = x.Email }).OrderBy(x => x.Email), _headerService.GetConversationId());
            }
        }
    }
}