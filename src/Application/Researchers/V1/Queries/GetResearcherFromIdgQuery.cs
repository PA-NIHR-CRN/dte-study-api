using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.Researchers;
using Application.Responses.V1.Researchers;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Study.Management.Api.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Researchers.V1.Queries
{
    public class GetResearcherFromIdgQuery : IRequest<Response<ResearcherResponse>>
    {
        public string Email { get; }

        public GetResearcherFromIdgQuery(string email)
        {
            Email = email;
        }

        public class GetResearcherFromIdgQueryHandler : IRequestHandler<GetResearcherFromIdgQuery, Response<ResearcherResponse>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetResearcherFromIdgQueryHandler> _logger;

            public GetResearcherFromIdgQueryHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<GetResearcherFromIdgQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<ResearcherResponse>> Handle(GetResearcherFromIdgQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _client.GetResearcherFromIdgAsync(request.Email);
                    
                    return response == null 
                        ? Response<ResearcherResponse>.CreateNotFoundResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetResearcherFromIdgQueryHandler), "err", $"Can't find researcher with email {request.Email}", _headerService.GetConversationId()) 
                        : Response<ResearcherResponse>.CreateSuccessfulContentResponse(ResearcherMapper.MapTo(response), _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<ResearcherResponse>.CreateHttpExceptionResponse(nameof(GetResearcherFromIdgQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting researcher from IDG {request.Email} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<ResearcherResponse>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetResearcherFromIdgQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error getting researcher from IDG {request.Email}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}