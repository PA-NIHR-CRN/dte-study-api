using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Dte.Common.Http;
using Dte.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.V1.Commands
{
    public class SaveAccessWhiteListCommand : IRequest<Response<object>>
    {
        public List<string> Emails { get; }

        public SaveAccessWhiteListCommand(List<string> emails)
        {
            Emails = emails;
        }

        public class SaveAccessWhiteListCommandHandler : IRequestHandler<SaveAccessWhiteListCommand, Response<object>>
        {
            private readonly IAccessWhitelistRepository _accessWhitelistRepository;
            private readonly IHeaderService _headerService;
            private readonly ILogger<SaveAccessWhiteListCommandHandler> _logger;

            public SaveAccessWhiteListCommandHandler(IAccessWhitelistRepository accessWhitelistRepository, IHeaderService headerService, ILogger<SaveAccessWhiteListCommandHandler> logger)
            {
                _accessWhitelistRepository = accessWhitelistRepository;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(SaveAccessWhiteListCommand request, CancellationToken cancellationToken)
            {
                await _accessWhitelistRepository.SaveWhitelist(request.Emails);

                return Response<object>.CreateSuccessfulResponse();
            }
        }
    }
}