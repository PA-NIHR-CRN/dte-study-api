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
    public class GetStudySitesPagedQuery : IRequest<Response<PaginationListResponse<StudySiteResponse>>>
    {
        public long StudyId { get; }
        public int Limit { get; set; }
        public string PaginationToken { get; set; }

        public GetStudySitesPagedQuery(long studyId, int limit, string paginationToken)
        {
            StudyId = studyId;
            Limit = limit;
            PaginationToken = paginationToken;
        }
        
        public class GetStudySitesPagedQueryHandler : IRequestHandler<GetStudySitesPagedQuery, Response<PaginationListResponse<StudySiteResponse>>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetStudySitesPagedQueryHandler> _logger;

            public GetStudySitesPagedQueryHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<GetStudySitesPagedQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<PaginationListResponse<StudySiteResponse>>> Handle(GetStudySitesPagedQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _client.GetStudySitesAsync(request.StudyId, request.Limit, request.PaginationToken);
                    
                    var paginationListResponse = new PaginationListResponse<StudySiteResponse>
                    {
                        PaginationToken = response.PaginationToken,
                        Items = response.Items.Select(StudySiteMapper.MapTo)
                    };
                    
                    return Response<PaginationListResponse<StudySiteResponse>>.CreateSuccessfulContentResponse(paginationListResponse, _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<PaginationListResponse<StudySiteResponse>>.CreateHttpExceptionResponse(nameof(GetStudySitesPagedQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting study sites for study Id: {request.StudyId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<PaginationListResponse<StudySiteResponse>>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetStudySitesPagedQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown getting study sites for study Id: {request.StudyId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}