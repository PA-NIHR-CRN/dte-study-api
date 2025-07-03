using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Mappings.Locations;
using Application.Responses.V1.Addresses;
using Dte.Common.Exceptions;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Location.Api.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Locations.V1.Queries
{
    public class GetAddressesByPostcodeQuery : IRequest<Response<IEnumerable<PostcodeAddressModel>>>
    {
        public string Postcode { get; }

        public GetAddressesByPostcodeQuery(string postcode)
        {
            Postcode = postcode;
        }

        public class GetAddressesByPostcodeQueryHandler : IRequestHandler<GetAddressesByPostcodeQuery, Response<IEnumerable<PostcodeAddressModel>>>
        {
            private readonly ILocationApiClient _client;
            private readonly IHeaderService _headerService;
            private readonly ILogger<GetAddressesByPostcodeQueryHandler> _logger;

            public GetAddressesByPostcodeQueryHandler(ILocationApiClient client, IHeaderService headerService, ILogger<GetAddressesByPostcodeQueryHandler> logger)
            {
                _client = client;
                _headerService = headerService;
                _logger = logger;
            }

            public async Task<Response<IEnumerable<PostcodeAddressModel>>> Handle(GetAddressesByPostcodeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = await _client.GetAddressesByPostcodeAsync(request.Postcode);

                    return Response<IEnumerable<PostcodeAddressModel>>.CreateSuccessfulContentResponse(response.Select(AddressMapper.MapTo), _headerService.GetConversationId());
                }
                catch (HttpServiceException ex)
                {
                    var exceptionResponse = Response<IEnumerable<PostcodeAddressModel>>.CreateHttpExceptionResponse(nameof(GetAddressesByPostcodeQueryHandler), ex, ErrorCode.UnableToGetAddressesFromLocationServiceError, _headerService.GetConversationId());
                    _logger.LogError(ex, "Error getting addresses for postcode {Postcode} - StatusCode: {HttpStatusCode}: {@exceptionResponse}", request.Postcode, ex.HttpStatusCode, exceptionResponse);
                    return exceptionResponse;
                }
                catch (Exception ex)
                {
                    var exceptionResponse = Response<IEnumerable<PostcodeAddressModel>>.CreateExceptionResponse(ProjectAssemblyNames.ApiAssemblyName, nameof(GetAddressesByPostcodeQueryHandler), ErrorCode.UnknownErrorGettingAddressesFromLocationServiceError, ex, _headerService.GetConversationId());
                    _logger.LogError(ex, "Unknown error getting addresses for postcode {Postcode}: {@exceptionResponse}", request.Postcode, exceptionResponse);
                    return exceptionResponse;
                }
            }
        }
    }
}