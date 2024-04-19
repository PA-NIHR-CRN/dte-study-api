using BPOR.Domain.Entities;
using BPOR.Geolocation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Geolocation.Controllers;

public class GeolocationController(ParticipantDbContext context, IGeolocationService geolocationService) : Controller
{
    [HttpGet]
    [Route("api/participants")]
    public async Task<IActionResult> AddLatLngToAllUsers(CancellationToken cancellationToken = default)
    {
        var participants = await context.Participants
            .Include(p => p.Address)
            .Take(10)
            .ToListAsync(cancellationToken);
        
        // loop through participants and add lat/lng to each
        foreach (var participant in participants)
        {
            if (participant.Address != null)
            {
                var postcode = participant.Address.Postcode;
                if (postcode != null)
                {
                    var latLng = await geolocationService.GetLatLngByPostcodeAsync(postcode, cancellationToken);
            
                    participant.Address.Latitude = latLng.Latitude;
                    participant.Address.Longitude = latLng.Longitude;
                }
            }
        }
        
        await context.SaveChangesAsync(cancellationToken);

        return Ok(participants);
    }
}
