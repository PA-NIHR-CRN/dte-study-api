using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dte.Common.Exceptions;
using MediatR;
using Dte.Study.Management.Api.Client;
using Dte.Common.Http;
using Microsoft.Extensions.Logging;
using Dte.Common.Responses;
using Dte.Common.Extensions;
using Newtonsoft.Json;
using System;
using Application.Constants;
using Dte.Study.Management.Api.Client.Request.Studies;

namespace Application.Studies.V1.Commands.Studies
{
    public class UpdateStudyCommand : IRequest<Response<object>>
    {
        public long StudyId { get; set; }
        public string WhatImportant { get; set; }
        public IEnumerable<string> HealthConditions { get; set; }
        public string ResearcherId { get; set; }


        public UpdateStudyCommand(long studyId,string researcherId, string whatImportant, IEnumerable<string> healthConditions)
        {
            StudyId = studyId;
            WhatImportant = whatImportant;
            ResearcherId = researcherId;
            HealthConditions = healthConditions;
        }

        public class UpdateStudyCommandHandler : IRequestHandler<UpdateStudyCommand, Response<object>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<UpdateStudyCommandHandler> _logger;


            public UpdateStudyCommandHandler(IStudyManagementApiClient client,IHeaderService headerService, ILogger<UpdateStudyCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(UpdateStudyCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.UpdateStudyAsync(request.StudyId, new UpdateStudyRequest { WhatImportant = request.WhatImportant, ResearcherId = request.ResearcherId, HealthConditions = request.HealthConditions});
                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(UpdateStudyCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error updating study: {request.StudyId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(UpdateStudyCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error updating study: {request.StudyId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }

            }
        }
    }
}