using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.Studies;
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
    public class GetStudyQuery : IRequest<Response<StudyRoleResponse>>
    {
        public long StudyId { get; }
        public string ResearcherId { get; set; }

        public GetStudyQuery(long studyId, string researcherId)
        {
            StudyId = studyId;
            ResearcherId = researcherId;
        }
        
        public class GetStudyQueryHandler : IRequestHandler<GetStudyQuery, Response<StudyRoleResponse>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetStudyQueryHandler> _logger;

            public GetStudyQueryHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<GetStudyQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<StudyRoleResponse>> Handle(GetStudyQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _client.GetStudyAsync(request.StudyId, request.ResearcherId);
                    
                    return Response<StudyRoleResponse>.CreateSuccessfulContentResponse(StudyMapper.MapTo(response), _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<StudyRoleResponse>.CreateHttpExceptionResponse(nameof(GetStudyQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting study id: {request.StudyId} for researcher Id: {request.ResearcherId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<StudyRoleResponse>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetStudyQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown getting study id: {request.StudyId} for researcher Id: {request.ResearcherId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}