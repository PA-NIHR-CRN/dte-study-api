using NIHR.Infrastructure.Clients;
using NIHR.Infrastructure.Models;

namespace BPOR.Geolocation.Services;

public class GeolocationService(ILocationApiClient locationApiClient): IGeolocationService
{
    public async Task<LatLngModel> GetLatLngByPostcodeAsync(string postcode, CancellationToken cancellationToken)
    {
        var latLng = await locationApiClient.GetLatLngByPostcodeAsync(postcode, cancellationToken);
        return latLng;
    }
}
