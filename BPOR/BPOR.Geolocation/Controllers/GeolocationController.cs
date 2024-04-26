using System.Diagnostics;
using BPOR.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure;

namespace BPOR.Geolocation.Controllers;

public class GeolocationController(ParticipantDbContext context, IPostcodeMapper locationApiClient) : Controller
{
    [HttpGet]
    [Route("api/add-lat-lng-to-all-users")]
    public async Task<IActionResult> AddCoordinatesToUsersWithPostcodes(CancellationToken cancellationToken = default)
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

    [HttpGet]
    [Route("api/add-random-coordinates-to-all-users")]
    public async Task<IActionResult> AddCoordinatesToAllUsers(CancellationToken cancellationToken = default)
    {
        const int batchSize = 1000;
        int numberOfBatches = (int)Math.Ceiling(context.Participants.Count() / (double)batchSize);

        for (int batchNumber = 0; batchNumber < numberOfBatches; batchNumber++)
        {
            var participantsBatch = context.Participants
                .Skip(batchNumber * batchSize)
                .Take(batchSize)
                .ToList();

            foreach (var participant in participantsBatch)
            {
                var randomCoordinates = GenerateRandomCoordinatesForUK();
                participant.ParticipantLocation = new ParticipantLocation
                {
                    Location = new Point(randomCoordinates.longitude, randomCoordinates.latitude) { SRID = 4326 }
                };
            }

            await context.SaveChangesAsync(cancellationToken);
        }

        return Ok($"Coordinates added to all participants.");
    }

    private static (double latitude, double longitude) GenerateRandomCoordinatesForUK()
    {
        Random random = new Random();
        // Approximate bounding box of the UK (lat, long)
        double minLatitude = 50.0, maxLatitude = 58.6;
        double minLongitude = -8.0, maxLongitude = 1.7;

        double latitude = random.NextDouble() * (maxLatitude - minLatitude) + minLatitude;
        double longitude = random.NextDouble() * (maxLongitude - minLongitude) + minLongitude;

        return (latitude, longitude);
    }

    // //GetParticipantsByPostcodePrefix
    // [HttpGet]
    // [Route("api/get-participants-by-postcode-prefix")]
    // public async Task<IActionResult> GetParticipantsByPostcodePrefix(string postcodePrefix, CancellationToken cancellationToken = default)
    // {
    //     var postcodePrefixes = postcodePrefix.Split(',').ToList();
    //     var participants = await context.GetParticipantsByPostcodePrefix(postcodePrefixes).ToListAsync(cancellationToken);
    //
    //     return Ok(participants);
    // }
    //
    // GetParticipantsWithinRadius
    [HttpGet]
    [Route("api/get-participants-within-radius")]
    public async Task<IActionResult> GetParticipantsWithinRadius(string postcode, double radiusInMiles,
        CancellationToken cancellationToken = default)
    {
        var coordinates = await locationApiClient.GetCoordinatesFromPostcodeAsync(postcode, cancellationToken);

        var point = new Point(coordinates.Longitude, coordinates.Latitude) { SRID = 4326 };

        var distanceInMeters = radiusInMiles * 1609.344;
        var boundingBox = point.Buffer(distanceInMeters / 111320).Envelope;
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var count = await context.ParticipantLocation
            .Where(x => x.Location.Within(boundingBox) && x.Location.IsWithinDistance(point, distanceInMeters))
            .CountAsync(cancellationToken);
        
        stopwatch.Stop();
        
        
        var response = new
        {
            TimeTaken = stopwatch.ElapsedMilliseconds + " ms",

        };


        return Ok(response);
    }
}
