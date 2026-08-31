using Amazon.Runtime.Internal.Util;
using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quartz;

namespace BPOR.Rms.Jobs;

public class RemoveStaleDraftStudiesJob(
    ILogger<RemoveStaleDraftStudiesJob> logger,
    IOptions<DraftStudiesSettings> settings,
    ParticipantDbContext dbContext)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var deletedCount = await dbContext.Studies
            .Where(i => i.UpdatedAt < DateTime.UtcNow.Subtract(settings.Value.StaleDraftAge) &&
                        false /*i.Status == StudyStatus.Draft*/)
            .ExecuteDeleteAsync(context.CancellationToken);
        logger.LogInformation("Deleted {deletedCount} stale draft studies", deletedCount);
    }
}

public class DraftStudiesSettings
{
    public string StaleDraftRemovalSchedule { get; set; } = "0 0 2 1/1 * ? *"; // Every day at 02:00
    public TimeSpan StaleDraftAge { get; set; } =  TimeSpan.FromDays(2);
}