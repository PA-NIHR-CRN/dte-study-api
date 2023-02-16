using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.FeatureFlags;
using Application.Responses.V1.FeatureFlags;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Reference.Data.Api.Client;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.ReferenceData.V1.Queries
{
    public class GetFeatureFlagQuery : IRequest<Response<FeatureFlagResponse>>
    {
        public string ServiceName { get; }
        public string FeatureName { get; }

        public GetFeatureFlagQuery(string serviceName, string featureName)
        {
            ServiceName = serviceName;
            FeatureName = featureName;
        }

        public class GetFeatureFlagQueryHandler : IRequestHandler<GetFeatureFlagQuery, Response<FeatureFlagResponse>>
        {
            private readonly IReferenceDataApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetFeatureFlagQueryHandler> _logger;

            public GetFeatureFlagQueryHandler(IReferenceDataApiClient client, IHeaderService headerService, ILogger<GetFeatureFlagQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<FeatureFlagResponse>> Handle(GetFeatureFlagQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return Response<FeatureFlagResponse>.CreateSuccessfulContentResponse(FeatureFlagMapper.MapTo(await _client.GetFeatureFlagAsync(request.ServiceName, request.FeatureName)));
                }
                catch (HttpServiceException ex)
                {
                    _logger.LogError(ex, $"HTTP Error getting feature flag for {request.ServiceName} : {request.FeatureName}");
                    return Response<FeatureFlagResponse>.CreateHttpExceptionResponse(nameof(GetFeatureFlagQueryHandler), ex, "err", _headerService.GetConversationId());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Unknown Error getting feature flag for {request.ServiceName} : {request.FeatureName}");
                    return Response<FeatureFlagResponse>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetFeatureFlagQueryHandler), "err", ex, _headerService.GetConversationId());
                }  
            }
        }
    }
}