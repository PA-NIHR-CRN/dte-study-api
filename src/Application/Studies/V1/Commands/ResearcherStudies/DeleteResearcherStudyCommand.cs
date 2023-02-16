using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Study.Management.Api.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Studies.V1.Commands.ResearcherStudies
{
    public class DeleteResearcherStudyCommand : IRequest<Response<object>>
    {
        public string UserId { get; set; }
        public string ResearcherId { get; }
        public long StudyId { get; }

        public DeleteResearcherStudyCommand(string userId, string researcherId, long studyId)
        {
            UserId = userId;
            ResearcherId = researcherId;
            StudyId = studyId;
        }
        
        public class DeleteResearcherStudyCommandHandler : IRequestHandler<DeleteResearcherStudyCommand, Response<object>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<DeleteResearcherStudyCommandHandler> _logger;

            public DeleteResearcherStudyCommandHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<DeleteResearcherStudyCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }
            
            public async Task<Response<object>> Handle(DeleteResearcherStudyCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.DeleteResearcherStudyAsync(request.StudyId, request.ResearcherId, request.UserId);
                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(DeleteResearcherStudyCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error deleting researcher study for study id: {request.StudyId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(DeleteResearcherStudyCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error deleting researcher study for study id: {request.StudyId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}