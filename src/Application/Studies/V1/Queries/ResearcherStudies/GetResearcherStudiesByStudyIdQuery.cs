using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.Studies;
using Application.Models.ResearcherStudies;
using Application.Responses;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Study.Management.Api.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Studies.V1.Queries.ResearcherStudies
{
    public class GetResearcherStudiesByStudyIdQuery : IRequest<Response<PaginationListResponse<ResearcherStudyModel>>>
    {
        public long StudyId { get; }
        public string UserId { get; }
        public int Limit { get; set; }
        public string PaginationToken { get; set; }

        public GetResearcherStudiesByStudyIdQuery(long studyId, string userId, int limit, string paginationToken)
        {
            StudyId = studyId;
            UserId = userId;
            Limit = limit;
            PaginationToken = paginationToken;
        }
        
        public class GetResearcherStudiesByStudyIdQueryHandler : IRequestHandler<GetResearcherStudiesByStudyIdQuery, Response<PaginationListResponse<ResearcherStudyModel>>>
        {
            private readonly IStudyManagementApiClient _studyManagementApiClient;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetResearcherStudiesByStudyIdQueryHandler> _logger;

            public GetResearcherStudiesByStudyIdQueryHandler(IStudyManagementApiClient studyManagementApiClient, IHeaderService headerService, ILogger<GetResearcherStudiesByStudyIdQueryHandler> logger)
            {
                _studyManagementApiClient = studyManagementApiClient;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<PaginationListResponse<ResearcherStudyModel>>> Handle(GetResearcherStudiesByStudyIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _studyManagementApiClient.GetResearcherStudiesByStudyIdAsync(request.StudyId, request.UserId, request.Limit, request.PaginationToken);

                    return Response<PaginationListResponse<ResearcherStudyModel>>.CreateSuccessfulContentResponse(new PaginationListResponse<ResearcherStudyModel>
                    {
                        PaginationToken = response.PaginationToken, Items = response.Items.Select(ResearcherStudyMapper.MapTo)
                    }, _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<PaginationListResponse<ResearcherStudyModel>>.CreateHttpExceptionResponse(nameof(GetResearcherStudiesByStudyIdQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting researcher studies with id {request.StudyId} for logged in researcher - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<PaginationListResponse<ResearcherStudyModel>>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetResearcherStudiesByStudyIdQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error getting researcher study with id {request.StudyId} for logged in researcher\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}