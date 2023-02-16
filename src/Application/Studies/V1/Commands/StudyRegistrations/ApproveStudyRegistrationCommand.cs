using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.StudyRegistrations;
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
    public class ApproveStudyRegistrationCommand : IRequest<Response<object>>
    {
        public long StudyId { get; }
        public string Title { get; set; }
        public long CpmsId { get; set; }
        public string IsrctnId { get; set; }
        public string UserId { get; }

        public ApproveStudyRegistrationCommand(long studyId, string title, long cpmsId, string isrctnId, string userId)
        {
            StudyId = studyId;
            Title = title;
            CpmsId = cpmsId;
            IsrctnId = isrctnId;
            UserId = userId;
        }

        public class ApproveStudyRegistrationCommandHandler : IRequestHandler<ApproveStudyRegistrationCommand, Response<object>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<ApproveStudyRegistrationCommandHandler> _logger;

            public ApproveStudyRegistrationCommandHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<ApproveStudyRegistrationCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(ApproveStudyRegistrationCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.ApproveStudyRegistrationAsync(request.StudyId, StudyRegistrationMapper.MapTo(request));
                    
                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(ApproveStudyRegistrationCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error approving study for study Id: {request.StudyId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(ApproveStudyRegistrationCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown approving study for study Id: {request.StudyId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}