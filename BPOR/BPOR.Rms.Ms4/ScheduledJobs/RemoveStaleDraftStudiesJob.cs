using BPOR.Domain.Entities;
using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using BPOR.Rms.Ms4.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace BPOR.Rms.Ms4.ScheduledJobs;

public class RemoveStaleDraftStudiesJob(
    ILogger<RemoveStaleDraftStudiesJob> logger,
    IOptions<StudyCreationSettings> settings,
    ParticipantDbContext dbContext)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var updateThreshold = DateTime.UtcNow.Subtract(settings.Value.StaleDraftAge);
        var deletedCount = await dbContext.Studies
            .Where(i => i.UpdatedAt < updateThreshold && i.StudyStatusId == StudyStatusType.Draft)
            .ExecuteDeleteAsync(context.CancellationToken);
        logger.LogInformation("Deleted {deletedCount} stale draft studies", deletedCount);
    }
}