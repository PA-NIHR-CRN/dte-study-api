using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BPOR.Rms.VolunteerInformation.Data;

public class VipSyncInterceptor(IVipRepository vipRepository) : ISaveChangesInterceptor
{
    record StudyValues(int Id, string StudyName, DateTime? RecruitmentEndDate );

    private readonly List<StudyValues> _updatedRecords = new();
        
    public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context != null)
        {
            var studies = eventData.Context.ChangeTracker.Entries<Study>();
            foreach (var study in studies.Where(s => s.State == EntityState.Modified
                                                     && HaveAnyChanged(s, nameof(Study.StudyName),
                                                         nameof(Study.RecruitmentEndDate))))
            {
                _updatedRecords.Add(new StudyValues(study.Entity.Id, study.Entity.StudyName,
                    study.Entity.RecruitmentEndDate));
            }
        }

        return ValueTask.FromResult(result);
    }

    private static bool HaveAnyChanged(EntityEntry entry, params string[] propertyNames)
    {
        foreach (var propertyName in propertyNames)
        {
            var propertyEntry = entry.Property(propertyName);
            if (propertyEntry?.IsModified == true)
            {
                return true;
            }
        }
        
        return false;
    }

    public async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = default)
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