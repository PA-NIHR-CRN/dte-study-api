using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BPOR.Domain.Entities;
using BPOR.Domain.Entities.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure;
using Polly;
using Polly.RateLimit;

namespace BPOR.Geolocation.Controllers;

public class GeolocationController(ParticipantDbContext context, IPostcodeMapper locationApiClient) : Controller
{
    public async Task<IActionResult> AddCoordinatesToUsersWithPostcodes(CancellationToken cancellationToken = default)
    {
        var cache = new ConcurrentDictionary<string, (double Latitude, double Longitude)>();

        var existingCoordinates = await context.Participants
            .Where(p => p.ParticipantLocation != null)
            .Select(p => new
            {
                p.Address.Postcode,
                p.ParticipantLocation.Location.Coordinate.Y,
                p.ParticipantLocation.Location.Coordinate.X
            })
            .ToListAsync(cancellationToken);

        foreach (var coordinate in existingCoordinates)
        {
            if (!string.IsNullOrEmpty(coordinate.Postcode))
            {
                cache.TryAdd(coordinate.Postcode, (coordinate.Y, coordinate.X));
            }
        }

        var rateLimitPolicy = Policy.RateLimitAsync(600, TimeSpan.FromMinutes(1));
        var retryPolicy = Policy.Handle<RateLimitRejectedException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2));

        var participants = new List<Participant>();

        var tasks = new List<Task>();

        await foreach (var participant in GetParticipantsWithAddressesAndWithoutLocationsAsync(cancellationToken))
        {
            tasks.Add(AddCoordinateToCache(participant, cache, rateLimitPolicy, retryPolicy, cancellationToken));
            participants.Add(participant);
        }

        await Task.WhenAll(tasks);

        // Update participant records sequentially
        foreach (var participant in participants)
        {
            if (cache.TryGetValue(participant.Address.Postcode, out var latLng))
            {
                participant.ParticipantLocation = ParticipantLocation.FromLatLong(latLng.Latitude, latLng.Longitude);
            }
        }

        await context.SaveChangesAsync(cancellationToken);

        return Ok("Coordinates added to all participants.");
    }

    private async Task AddCoordinateToCache(Participant participant,
        ConcurrentDictionary<string, (double Latitude, double Longitude)> cache,
        IAsyncPolicy rateLimitPolicy, IAsyncPolicy retryPolicy,
        CancellationToken cancellationToken)
    {
        var postcode = participant.Address.Postcode;
        if (postcode != null)
        {
            if (!cache.TryGetValue(postcode, out var latLng))
            {
                await rateLimitPolicy.ExecuteAsync(async () =>
                {
                    await retryPolicy.ExecuteAsync(async () =>
                    {
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
                    });
                });
            }
        }
    }

    private async IAsyncEnumerable<Participant> GetParticipantsWithAddressesAndWithoutLocationsAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var participant in context.Participants
                           .Include(p => p.Address)
                           .Where(p => p.ParticipantLocation == null)
                           .AsAsyncEnumerable()
                           .WithCancellation(cancellationToken))
        {
            yield return participant;
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
                participant.ParticipantLocation = ParticipantLocation.FromLatLong(randomCoordinates.latitude, randomCoordinates.longitude);
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

        var point = new Point(coordinates.Longitude, coordinates.Latitude) { SRID = ParticipantLocationConfiguration.LocationSrid };

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
