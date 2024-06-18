using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NIHR.Infrastructure.Clients;

namespace BPOR.Registration.Api.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/location")]
[Authorize("AnyAuthenticatedUser")]
public class LocationController(ILocationApiClient locationApiClient) : Controller
{
    [HttpGet("postcode/{postcode}")]
    public async Task<IActionResult> GetAddressesByPostcode(string postcode, CancellationToken cancellationToken)
    {
        return Ok(await locationApiClient.GetAddressesByPostcodeAsync(postcode, cancellationToken));
    }
}
