using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.VolunteerInformation.Data;

public class StudyDbRepository(ParticipantDbContext db) : IStudyRepository
{
    public async Task<Study?> GetStudy(int studyId, CancellationToken cancellationToken)
    {
        return await db.Studies.SingleOrDefaultAsync(i => i.Id == studyId && !i.IsDeleted,
            cancellationToken: cancellationToken);
    }

    public async Task<bool> UpdateStudy(int studyId, Action<Study> action, CancellationToken cancellationToken)
    {
        var study = await db.Studies.SingleOrDefaultAsync(i => i.Id == studyId, cancellationToken);
        if (study == null)
        {
            return false;
        }
        action(study);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}