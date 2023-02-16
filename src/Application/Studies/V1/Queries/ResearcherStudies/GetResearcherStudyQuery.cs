using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.Studies;
using Application.Models.ResearcherStudies;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Study.Management.Api.Client;
using Dte.Study.Management.Api.Client.Responses.ResearcherStudies;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Studies.V1.Queries.ResearcherStudies
{
    public class GetResearcherStudyQuery : IRequest<Response<ResearcherStudyModel>>
    {
        public string ResearcherId { get; set; }
        public long StudyId { get; set; }

        public GetResearcherStudyQuery(string researcherId, long studyId)
        {
            ResearcherId = researcherId;
            StudyId = studyId;
        }
        
        public class GetResearcherStudyQueryHandler : IRequestHandler<GetResearcherStudyQuery, Response<ResearcherStudyModel>>
        {
            private readonly IStudyManagementApiClient _studyManagementApiClient;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetResearcherStudyQueryHandler> _logger;

            public GetResearcherStudyQueryHandler(IStudyManagementApiClient studyManagementApiClient, IHeaderService headerService, ILogger<GetResearcherStudyQueryHandler> logger)
            {
                _studyManagementApiClient = studyManagementApiClient;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<ResearcherStudyModel>> Handle(GetResearcherStudyQuery request, CancellationToken cancellationToken)
            {
                ResearcherStudyResponse response;
                
                try
                {
                    response = await _studyManagementApiClient.GetResearcherStudyAsync(request.StudyId, request.ResearcherId);
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<ResearcherStudyModel>.CreateHttpExceptionResponse(nameof(GetResearcherStudyQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting researcher study with id {request.StudyId} for logged in researcher - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<ResearcherStudyModel>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetResearcherStudyQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error getting researcher study with id {request.StudyId} for logged in researcher\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }

                return response == null 
                    ? Response<ResearcherStudyModel>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetResearcherStudyQueryHandler), "Study_Registration_Not_Found_Error", $"Could not find researcher study with id {request.StudyId} for logged in researcher", _headerService.GetConversationId()) 
                    : Response<ResearcherStudyModel>.CreateSuccessfulContentResponse(ResearcherStudyMapper.MapTo(response), _headerService.GetConversationId());
            }
        }
    }
}