using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Locations.V1.Queries;
using Application.Responses.V1.Addresses;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApi.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/location")]
[Authorize("AnyAuthenticatedUser")]
public class LocationController : Controller
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// [Authorize("AnyAuthenticatedUser")] Get addresses by postcode
    /// </summary>
    /// <response code="200">Addresses returned when IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<IEnumerable<PostcodeAddressModel>>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpGet("postcode/{postcode}")]
    public async Task<IActionResult> GetAddressesByPostcode(string postcode)
    {
        return Ok(await _locationService.GetAddressesByPostcodeAsync(postcode));
    }
}
