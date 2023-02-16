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
    public class CreateResearcherStudyCommand : IRequest<Response<object>>
    {
        public string UserId { get; set; }
        public string ResearcherId { get; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public long StudyId { get; }
        public ResearcherStudyRole Role { get; }

        public CreateResearcherStudyCommand(string userId, string researcherId, string firstname, string lastname, string email, long studyId, ResearcherStudyRole role)
        {
            UserId = userId;
            ResearcherId = researcherId;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            StudyId = studyId;
            Role = role;
        }
        
        public class CreateResearcherStudyCommandHandler : IRequestHandler<CreateResearcherStudyCommand, Response<object>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<CreateResearcherStudyCommandHandler> _logger;

            public CreateResearcherStudyCommandHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<CreateResearcherStudyCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }
            
            public async Task<Response<object>> Handle(CreateResearcherStudyCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.CreateResearcherStudyAsync(new CreateResearcherStudyRequest
                    {
                        UserId = request.UserId,
                        ResearcherId = request.ResearcherId,
                        ResearcherFirstname = request.Firstname,
                        ResearcherLastname = request.Lastname,
                        ResearcherEmail = request.Email,
                        StudyId = request.StudyId,
                        Role = (Dte.Study.Management.Api.Client.Models.ResearcherStudyRole)request.Role
                    });
                    
                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(CreateResearcherStudyCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error creating researcher study for study id: {request.StudyId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(CreateResearcherStudyCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error creating researcher study for study id: {request.StudyId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}