using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BPOR.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure;

namespace BPOR.Geolocation.Controllers;

public class GeolocationController(ParticipantDbContext context, IPostcodeMapper locationApiClient) : Controller
{
    public async Task<IActionResult> AddCoordinatesToUsersWithPostcodes(CancellationToken cancellationToken = default)
    {
        var cache = new ConcurrentDictionary<string, (double Latitude, double Longitude)>();
        var semaphore = new SemaphoreSlim(600, 600);
        var tasks = new List<Task>();

        await foreach (var participant in GetParticipantsWithAddressesAsync(cancellationToken))
        {
            tasks.Add(ProcessParticipant(participant, cache, semaphore, cancellationToken));
        }

        await Task.WhenAll(tasks);
        await context.SaveChangesAsync(cancellationToken);

        return Ok("Coordinates added to all participants.");
    }

    private async Task ProcessParticipant(Participant participant,
        ConcurrentDictionary<string, (double Latitude, double Longitude)> cache, SemaphoreSlim semaphore,
        CancellationToken cancellationToken)
    {
        var postcode = participant.Address.Postcode;
        if (postcode != null)
        {
            if (!cache.TryGetValue(postcode, out var latLng))
            {
                await semaphore.WaitAsync(cancellationToken);
                try
                {
                    // Recheck after acquiring the semaphore
                    if (!cache.TryGetValue(postcode, out latLng))
                    {
                        var coordinates =
                            await locationApiClient.GetCoordinatesFromPostcodeAsync(postcode, cancellationToken);
                        if (coordinates != null)
                        {
                            latLng = (coordinates.Latitude, coordinates.Longitude);
                            cache[postcode] = latLng;
                        }
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }

            if (latLng != default)
            {
                participant.ParticipantLocation = new ParticipantLocation
                {
                    Location = new Point(latLng.Longitude, latLng.Latitude) { SRID = 4326 }
                };
            }
        }
    }


    private async IAsyncEnumerable<Participant> GetParticipantsWithAddressesAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var participant in context.Participants.Include(p => p.Address).AsAsyncEnumerable()
                           .WithCancellation(cancellationToken))
        {
            if (participant.Address != null)
            {
                yield return participant;
            }
        }
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
