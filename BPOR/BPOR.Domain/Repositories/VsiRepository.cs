using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BPOR.Domain.Repositories;

public class VsiRepository(ParticipantDbContext db, IOptions<VsiSettings> options)
{
    public async Task<long?> GetActiveDraftId(int studyId, CancellationToken cancellationToken)
    {
        DateTime minUpdateTime = DateTime.UtcNow.Subtract(options.Value.DraftTtl);

        var query = db.VolunteerStudyInformation.Where(vsi =>
            vsi.IsDeleted == false &&
            vsi.StudyId == studyId &&
            vsi.StatusId == VolunteerStudyInformationStatusId.Draft &&
            vsi.UpdatedAt > minUpdateTime)
            .Select(vsi => (int?)vsi.Id);
        
        // The query should only ever return at most a single row, but the db schema does not guarantee it.
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<VolunteerStudyInformation?> GetActiveDraft(int studyId, int vsiId,
        CancellationToken cancellationToken)
    {
        return GetActiveDraft<VolunteerStudyInformation>(studyId, vsiId, i => i, cancellationToken);
    }

    public async Task<T?> GetActiveDraft<T>(long studyId, long vsiId,
        Expression<Func<VolunteerStudyInformation, T>> selector,
        CancellationToken cancellationToken)
    {
        DateTime minUpdateTime = DateTime.UtcNow.Subtract(options.Value.DraftTtl);

        var query = db.VolunteerStudyInformation.Where(vsi =>
            vsi.IsDeleted == false &&
            vsi.StudyId == studyId &&
            vsi.Id == vsiId &&
            vsi.StatusId == VolunteerStudyInformationStatusId.Draft &&
            vsi.UpdatedAt > minUpdateTime);
        
        var projection = query.Select(selector);
        
        // The query should only ever return at most a single row, but the db schema does not guarantee it.
        return await projection.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) 
        => db.SaveChangesAsync(cancellationToken);
}