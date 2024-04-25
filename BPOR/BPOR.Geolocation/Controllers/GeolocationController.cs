using BPOR.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure.Clients;

namespace BPOR.Geolocation.Controllers;

public class GeolocationController(ParticipantDbContext context, ILocationApiClient locationApiClient) : Controller
{
    [HttpGet]
    [Route("api/add-lat-lng-to-all-users")]
    public async Task<IActionResult> AddLatLngToAllUsers(CancellationToken cancellationToken = default)
    {
        var participants = await context.Participants
            .Include(p => p.Address)
            .Take(20)
            .ToListAsync(cancellationToken);

        // filter out participants with no address
        participants = participants.Where(p => p.Address != null).ToList();

        // loop through participants and add lat/lng to each
        foreach (var participant in participants)
        {
            if (participant.Address != null)
            {
                var postcode = participant.Address.Postcode;
                if (postcode != null)
                {
                    var coordinates =
                        await locationApiClient.GetCoordinatesFromPostcodeAsync(postcode, cancellationToken);

                    if (coordinates != null)
                    {
                        participant.ParticipantLocation = new ParticipantLocation
                        {
                            Location = new Point(coordinates.Latitude, coordinates.Longitude) { SRID = 4326 }
                        };
                    }
                }
            }
        }

        await context.SaveChangesAsync(cancellationToken);

        return Ok(participants);
    }

    //GetParticipantsByPostcodePrefix
    [HttpGet]
    [Route("api/get-participants-by-postcode-prefix")]
    public IActionResult GetParticipantsByPostcodePrefix(string postcodePrefix)
    {
        var postcodePrefixes = postcodePrefix.Split(',').ToList();
        var participants = context.GetParticipantsByPostcodePrefix(postcodePrefixes);

        return Ok(participants);
    }

    // GetParticipantsWithinRadius
    [HttpGet]
    [Route("api/get-participants-within-radius")]
    public async Task<IActionResult> GetParticipantsWithinRadius(string postcode, double radius,
        CancellationToken cancellationToken = default)
    {
        var coordinates = await locationApiClient.GetCoordinatesFromPostcodeAsync(postcode, cancellationToken);

        var point = new Point(coordinates.Latitude, coordinates.Longitude) { SRID = 4326 };
        var participants = await context.GetParticipantsWithinRadius(point, radius).ToListAsync(cancellationToken);

        return Ok(participants);
    }
}
