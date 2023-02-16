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
    public class StartReviewStudyRegistrationCommand : IRequest<Response<object>>
    {
        public long StudyId { get; }
        public string UserId { get; }

        public StartReviewStudyRegistrationCommand(long studyId, string userId)
        {
            StudyId = studyId;
            UserId = userId;
        }
        
        public class StartReviewStudyRegistrationCommandHandler : IRequestHandler<StartReviewStudyRegistrationCommand, Response<object>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<StartReviewStudyRegistrationCommandHandler> _logger;

            public StartReviewStudyRegistrationCommandHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<StartReviewStudyRegistrationCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(StartReviewStudyRegistrationCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.StartReviewStudyRegistrationAsync(request.StudyId);
                    
                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(StartReviewStudyRegistrationCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error starting review for study Id: {request.StudyId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(StartReviewStudyRegistrationCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown starting review for study Id: {request.StudyId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}