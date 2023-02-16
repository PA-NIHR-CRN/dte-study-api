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

namespace Application.Studies.V1.Commands.StudyRegistrations
{
    public class RejectStudyRegistrationCommand : IRequest<Response<object>>
    {
        public long StudyId { get; }
        public string UserId { get; }

        public RejectStudyRegistrationCommand(long studyId, string userId)
        {
            StudyId = studyId;
            UserId = userId;
        }
        
        public class RejectStudyRegistrationCommandHandler : IRequestHandler<RejectStudyRegistrationCommand, Response<object>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<RejectStudyRegistrationCommandHandler> _logger;

            public RejectStudyRegistrationCommandHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<RejectStudyRegistrationCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(RejectStudyRegistrationCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.RejectStudyRegistrationAsync(request.StudyId, request.UserId);
                    
                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(RejectStudyRegistrationCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error rejecting study for study Id: {request.StudyId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(RejectStudyRegistrationCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown rejecting study for study Id: {request.StudyId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}