using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Application.Mappings.Locations;
using Application.Responses.V1.Addresses;
using Dte.Common.Exceptions;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Dte.Location.Api.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class LocationService : ILocationService
{
    private readonly ILocationApiClient _client;
    private readonly IHeaderService _headerService;
    private readonly ILogger<LocationService> _logger;

    public LocationService(ILocationApiClient client, IHeaderService headerService, ILogger<LocationService> logger)
    {
        _client = client;
        _headerService = headerService;
        _logger = logger;
    }

    public async Task<Response<IEnumerable<PostcodeAddressModel>>> GetAddressesByPostcodeAsync(string postcode)
    {
        try
        {
            var response = await _client.GetAddressesByPostcodeAsync(postcode);

            return Response<IEnumerable<PostcodeAddressModel>>.CreateSuccessfulContentResponse(
                response.Select(AddressMapper.MapTo), _headerService.GetConversationId());
        }
        catch (HttpServiceException ex)
        {
            var exceptionResponse = Response<IEnumerable<PostcodeAddressModel>>.CreateHttpExceptionResponse(
                nameof(LocationService), ex, ErrorCode.UnableToGetAddressesFromLocationServiceError,
                _headerService.GetConversationId());
            _logger.LogError(ex,
                "Error getting addresses for postcode {Postcode} - StatusCode: {ExHttpStatusCode}\\r\\n{SerializeObject}",
                postcode, ex.HttpStatusCode, JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented));
            return exceptionResponse;
        }
        catch (Exception ex)
        {
            var exceptionResponse = Response<IEnumerable<PostcodeAddressModel>>.CreateExceptionResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(LocationService),
                ErrorCode.UnknownErrorGettingAddressesFromLocationServiceError, ex, _headerService.GetConversationId());
            _logger.LogError(ex, "Unknown error getting addresses for postcode {Postcode}\\r\\n{SerializeObject}",
                postcode, JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented));
            return exceptionResponse;
        }
    }
}
