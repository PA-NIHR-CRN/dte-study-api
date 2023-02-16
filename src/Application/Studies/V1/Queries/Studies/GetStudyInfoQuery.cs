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
    public class GetStudyInfoQuery : IRequest<Response<StudyInfoResponse>>
    {
        public long StudyId { get; }

        public GetStudyInfoQuery(long studyId)
        {
            StudyId = studyId;
        }
        
        public class GetStudyInfoQueryHandler : IRequestHandler<GetStudyInfoQuery, Response<StudyInfoResponse>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetStudyInfoQueryHandler> _logger;

            public GetStudyInfoQueryHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<GetStudyInfoQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<StudyInfoResponse>> Handle(GetStudyInfoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _client.GetStudyInfoAsync(request.StudyId);

                    return Response<StudyInfoResponse>.CreateSuccessfulContentResponse(StudyMapper.MapTo(response), _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<StudyInfoResponse>.CreateHttpExceptionResponse(nameof(GetStudyInfoQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting study info for Id: {request.StudyId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<StudyInfoResponse>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetStudyInfoQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error getting study info for Id: {request.StudyId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}