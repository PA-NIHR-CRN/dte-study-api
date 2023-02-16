using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.ReferenceData;
using Application.Responses.V1.ReferenceData;
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
    public class GetDemographicsEthnicityQuery : IRequest<Response<Dictionary<string, EthnicityResponse>>>
    {
        public GetDemographicsEthnicityQuery()
        {
        }

        public class GetDemographicsEthnicityQueryHandler : IRequestHandler<GetDemographicsEthnicityQuery, Response<Dictionary<string, EthnicityResponse>>>
        {
            private readonly IReferenceDataApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetDemographicsEthnicityQueryHandler> _logger;

            public GetDemographicsEthnicityQueryHandler(IReferenceDataApiClient client, IHeaderService headerService, ILogger<GetDemographicsEthnicityQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<Dictionary<string, EthnicityResponse>>> Handle(GetDemographicsEthnicityQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _client.GetDemographicsEthnicityAsync();

                    return Response<Dictionary<string, EthnicityResponse>>.CreateSuccessfulContentResponse(DemographicsMapper.MapTo(response), _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<Dictionary<string, EthnicityResponse>>.CreateHttpExceptionResponse(nameof(GetDemographicsEthnicityQueryHandler), ex, "err", _headerService.GetConversationId());
                    _logger.LogError(ex, $"Error getting reference data demographics ethnicity - StatusCode: {ex.HttpStatusCode}\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<Dictionary<string, EthnicityResponse>>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetDemographicsEthnicityQueryHandler), "err", ex, _headerService.GetConversationId());
                    _logger.LogError(ex, $"Unknown error getting reference data demographics ethnicity\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
                    return exceptionResponse;
                }
            }
        }
    }
}