using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Domain.Entities.Studies;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Study.Management.Api.Client;
using Dte.Study.Management.Api.Client.Request.ResearcherStudies;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Studies.V1.Commands.ResearcherStudies
{
    public class UpdateResearcherStudyCommand : IRequest<Response<object>>
    {
        public string UserId { get; set; }
        public string ResearcherId { get; }
        public long StudyId { get; }
        public ResearcherStudyRole Role { get; }

        public UpdateResearcherStudyCommand(string userId, string researcherId, long studyId, ResearcherStudyRole role)
        {
            UserId = userId;
            ResearcherId = researcherId;
            StudyId = studyId;
            Role = role;
        }
        
        public class UpdateResearcherStudyCommandHandler : IRequestHandler<UpdateResearcherStudyCommand, Response<object>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<UpdateResearcherStudyCommandHandler> _logger;

            public UpdateResearcherStudyCommandHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<UpdateResearcherStudyCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }
            
            public async Task<Response<object>> Handle(UpdateResearcherStudyCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.UpdateResearcherStudyAsync(request.StudyId, request.ResearcherId, request.UserId, new UpdateResearcherStudyRequest
                    {
                        Role = (Dte.Study.Management.Api.Client.Models.ResearcherStudyRole)request.Role
                    });
                    
                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(UpdateResearcherStudyCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error updating researcher study for study id: {request.StudyId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(UpdateResearcherStudyCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error updating researcher study for study id: {request.StudyId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}