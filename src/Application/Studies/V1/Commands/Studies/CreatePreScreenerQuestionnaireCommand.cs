using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.Studies;
using Application.Models.Studies;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Study.Management.Api.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Studies.V1.Commands.Studies
{
    public class CreatePreScreenerQuestionnaireCommand : IRequest<Response<object>>
    {
        public long StudyId { get; set; }
        public int Version { get; set; }
        public IEnumerable<PreScreenerQuestionModel> PreScreenerQuestions { get; set; }
        public string ResearcherId { get; }

        public CreatePreScreenerQuestionnaireCommand(long studyId, int version, IEnumerable<PreScreenerQuestionModel> preScreenerQuestions, string researcherId)
        {
            StudyId = studyId;
            Version = version;
            PreScreenerQuestions = preScreenerQuestions;
            ResearcherId = researcherId;
        }

        public class CreatePreScreenerQuestionnaireCommandHandler : IRequestHandler<CreatePreScreenerQuestionnaireCommand, Response<object>>
        {
            private readonly IStudyManagementApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<CreatePreScreenerQuestionnaireCommandHandler> _logger;

            public CreatePreScreenerQuestionnaireCommandHandler(IStudyManagementApiClient client, IHeaderService headerService, ILogger<CreatePreScreenerQuestionnaireCommandHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<object>> Handle(CreatePreScreenerQuestionnaireCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _client.SavePreScreenerQuestionsAsync(StudyMapper.MapTo(request));
                    
                    return Response<object>.CreateSuccessfulResponse(_headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<object>.CreateHttpExceptionResponse(nameof(CreatePreScreenerQuestionnaireCommandHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error creating pre screener questions for study id: {request.StudyId} - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<object>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(CreatePreScreenerQuestionnaireCommandHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error creating pre screener questions for study id: {request.StudyId}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}