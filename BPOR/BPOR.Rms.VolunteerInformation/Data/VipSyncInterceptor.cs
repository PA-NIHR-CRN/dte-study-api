using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BPOR.Rms.VolunteerInformation.Data;

public class VipSyncInterceptor(IVipRepository vipRepository) : ISaveChangesInterceptor
{
    record StudyValues(int Id, string StudyName, DateTime? RecruitmentEndDate );

    private readonly List<StudyValues> _updatedRecords = new();
        
    public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken)
    {
        var studies = eventData.Context.ChangeTracker.Entries<Study>();
        foreach (var study in studies.Where(s => s.State == EntityState.Modified))
        {
            _updatedRecords.Add(new StudyValues(study.Entity.Id, study.Entity.StudyName, study.Entity.RecruitmentEndDate));
        }

        return ValueTask.FromResult(result);
    }

    public async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken)
    {
        foreach (var updatedRecord in _updatedRecords)
        {
            await vipRepository.UpdateVsiIfExists(updatedRecord.Id, vsi =>
                {
                    vsi.StudyName = updatedRecord.StudyName;
                    vsi.StudyRecruitmentEndDate = updatedRecord.RecruitmentEndDate;
                },
                cancellationToken);
        }
        
        return result;
    }
}