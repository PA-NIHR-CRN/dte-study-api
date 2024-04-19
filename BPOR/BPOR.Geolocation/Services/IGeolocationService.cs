using BPOR.Infrastructure.Models.Address;
using NIHR.Infrastructure.Models;

namespace BPOR.Geolocation.Services;

public interface IGeolocationService
{
    Task<LatLngModel> GetLatLngByPostcodeAsync(string postcode, CancellationToken cancellationToken);
}
