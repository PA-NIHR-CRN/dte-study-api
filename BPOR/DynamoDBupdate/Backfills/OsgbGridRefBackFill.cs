using BPOR.Domain.Entities;
using BPOR.Domain.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NIHR.Geometry;

namespace DynamoDBupdate.Backfills
{
    public class OsgbGridRefBackFill(ParticipantDbContext participantDbContext, ILogger<OsgbGridRefBackFill> logger,
            IOptions<OsSettings> osSettings)
    {
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            if (!osSettings.Value.RunOsgbGridRefBackfill)
            {
                logger.LogInformation("Not running {job}", nameof(OsgbGridRefBackFill));
                return;
            }

            while (true)
            {
                foreach (var participantLocation in participantDbContext.ParticipantLocation.Where(i => i.Easting == 0).Take(1000))
                {
                    participantLocation.UpdateOsgbToMatchLocation();
                }

                if (await participantDbContext.SaveChangesAsync() == 0)
                    break;
                participantDbContext.ChangeTracker.Clear();
            }
        }
    }
}
