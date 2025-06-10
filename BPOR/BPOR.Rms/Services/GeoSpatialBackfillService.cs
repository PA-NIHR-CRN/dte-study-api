
using BPOR.Domain.Entities;
using NIHR.Geometry;

namespace BPOR.Rms.Services
{
    public class GeoSpatialBackfillService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(true)
            {
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetService<ParticipantDbContext>();
                foreach (var participantLocation in db.ParticipantLocation.Where(i => i.Easting == 0).Take(1000))
                {
                    var osgbRef = Osgb.FromLongitudeLatitude(participantLocation.Location.X, participantLocation.Location.Y);
                    participantLocation.Easting = osgbRef.Easting;
                    participantLocation.Northing = osgbRef.Northing;
                }

                if (await db.SaveChangesAsync() == 0) 
                    break;
            }
        }
    }
}
