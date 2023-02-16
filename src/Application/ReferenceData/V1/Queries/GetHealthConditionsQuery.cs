using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Reference.Data.Api.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.ReferenceData.V1.Queries
{
    public class GetHealthConditionsQuery : IRequest<Response<string[]>>
    {
        public GetHealthConditionsQuery()
        {
        }

        public class GetHealthConditionsQueryHandler : IRequestHandler<GetHealthConditionsQuery, Response<string[]>>
        {
            private readonly IReferenceDataApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetHealthConditionsQueryHandler> _logger;

            public GetHealthConditionsQueryHandler(IReferenceDataApiClient client, IHeaderService headerService, ILogger<GetHealthConditionsQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<string[]>> Handle(GetHealthConditionsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _client.GetHealthConditionsAsync();

                    return Response<string[]>.CreateSuccessfulContentResponse(response, _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<string[]>.CreateHttpExceptionResponse(nameof(GetHealthConditionsQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting reference data health conditions - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<string[]>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetHealthConditionsQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error getting reference data health conditions\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}