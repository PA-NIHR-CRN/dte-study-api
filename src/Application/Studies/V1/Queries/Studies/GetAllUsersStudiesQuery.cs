using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.Studies;
using Application.Responses;
using Application.Responses.V1.Studies;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Study.Management.Api.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Studies.V1.Queries.Studies
{
    public class GetAllUsersStudiesQuery : IRequest<Response<PaginationListResponse<StudyResponse>>>
    {
        public string UserId { get; }
        public int Limit { get; set; }
        public string PaginationToken { get; set; }

        public GetAllUsersStudiesQuery(string userId, int limit, string paginationToken)
        {
            UserId = userId;
            Limit = limit;
            PaginationToken = paginationToken;
        }
        
        public class GetAllUsersStudiesQueryHandler : IRequestHandler<GetAllUsersStudiesQuery, Response<PaginationListResponse<StudyResponse>>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetAllUsersStudiesQueryHandler> _logger;

            public GetAllUsersStudiesQueryHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<GetAllUsersStudiesQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<PaginationListResponse<StudyResponse>>> Handle(GetAllUsersStudiesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _client.GetAllMyStudiesAsync(request.UserId, request.Limit, request.PaginationToken);
                    
                    var paginationListResponse = new PaginationListResponse<StudyResponse>
                    {
                        PaginationToken = response.PaginationToken,
                        Items = response.Items.Select(StudyMapper.MapTo)
                    };
                    
                    return Response<PaginationListResponse<StudyResponse>>.CreateSuccessfulContentResponse(paginationListResponse, _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<PaginationListResponse<StudyResponse>>.CreateHttpExceptionResponse(nameof(GetAllUsersStudiesQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting studies for user Id: {request.UserId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<PaginationListResponse<StudyResponse>>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetAllUsersStudiesQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown getting studies for user Id: {request.UserId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}