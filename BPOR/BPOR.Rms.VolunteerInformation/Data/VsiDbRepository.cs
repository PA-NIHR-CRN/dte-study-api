using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.VolunteerInformation.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BPOR.Rms.VolunteerInformation.Data;

public class VsiDbRepository(ParticipantDbContext db, IOptions<VsiSettings> options) : IVsiRepository
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

    public Task<VolunteerStudyInformation?> GetCurrentVsi(int studyId,
        CancellationToken cancellationToken)
    {
        return GetCurrentVsi<VolunteerStudyInformation>(studyId, i => i, cancellationToken);
    }

    public async Task<T?> GetCurrentVsi<T>(long studyId,
        Expression<Func<VolunteerStudyInformation, T>> selector,
        CancellationToken cancellationToken)
    {
        var query = WhereCurrentVsi(db.VolunteerStudyInformation, studyId);
        var projection = query.Select(selector);
        
        // The query should only ever return at most a single row, but the db schema does not guarantee it.
        return await projection.FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<T?> GetCurrentVsiGroup<T>(long studyId, long groupId,
        Expression<Func<VolunteerStudyInformationGroup, T>> selector,
        CancellationToken cancellationToken)
    {
        var query = db.VolunteerStudyInformationGroup
            .Where(group => group.Id == groupId && group.VolunteerStudyInformation.StudyId == studyId);
        var projection = query.Select(selector);
        
        return await projection.FirstOrDefaultAsync(cancellationToken);
    }
    
    private IQueryable<VolunteerStudyInformation> WhereCurrentVsi(IQueryable<VolunteerStudyInformation> query,
        long studyId)
    {
        DateTime minUpdateTime = DateTime.UtcNow.Subtract(options.Value.DraftTtl);
        
        return query.Where(vsi =>
            vsi.IsDeleted == false &&
            vsi.StudyId == studyId &&
            (
                (vsi.StatusId == VolunteerStudyInformationStatusId.Draft && vsi.UpdatedAt > minUpdateTime) ||
                vsi.StatusId == VolunteerStudyInformationStatusId.Active
            ));
    }

    public async Task<bool> RemoveGroup(int studyId, int groupid, CancellationToken cancellationToken)
    {
        var group = await db.VolunteerStudyInformationGroup.SingleOrDefaultAsync(i => i.Id == groupid &&
            i.VolunteerStudyInformation.StudyId == studyId, cancellationToken);
        if (group == null)
        {
            return false;
        }
        db.VolunteerStudyInformationGroup.Remove(group);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RemoveCriteria(int studyId, int groupId, int criteriaId, CancellationToken cancellationToken)
    {
        var criteria = await db.VolunteerStudyInformationGroupCriteria
            .SingleOrDefaultAsync(i => i.Id == criteriaId
                                       && i.VolunteerStudyInformationGroupId == groupId 
                                       && i.Group.VolunteerStudyInformation.StudyId == studyId,
                cancellationToken);

        if (criteria == null)
        {
            return false;
        }

        db.VolunteerStudyInformationGroupCriteria.Remove(criteria);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> CreateCriterion(int studyId, int groupId, VolunteerStudyInformationGroupCriteria newCriteria, CancellationToken cancellationToken)
    {
        var group = await GetCurrentVsiGroup(studyId, groupId, i => i, cancellationToken);
        if (group == null)
        {
            return false;
        }
        group.Criteria.Add(newCriteria);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateVsi(int studyId, Action<VolunteerStudyInformation> action, CancellationToken cancellationToken)
    {
        var vsi = await GetCurrentVsi(studyId, i => i, cancellationToken);
        if (vsi == null)
        {
            return false;
        }
        action(vsi);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public Task<bool> RemoveSite(int studyId, int siteId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<int?> CreateGroup(int studyId, string groupName, CancellationToken cancellationToken)
    {
        var vsi = await GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return null;
        }
        var newGroup = new VolunteerStudyInformationGroup(){Name = groupName};
        vsi.Groups.Add(newGroup);
        await db.SaveChangesAsync(cancellationToken);
        return newGroup.Id;
    }

    public async Task<int?> CreateSite(int studyId, VolunteerStudyInformationSite newSite, CancellationToken cancellationToken)
    {
        var vsi = await GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return null;
        }
        vsi.Sites.Add(newSite);
        await db.SaveChangesAsync(cancellationToken);
        return newSite.Id;
    }

    public Task CreateVsi(int studyId, VolunteerStudyInformationStatusId status, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<int?> CreateContact(int studyId, VolunteerStudyInformationContact newContact, CancellationToken cancellationToken)
    {
        var vsi = await GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return null;
        }
        vsi.Contacts.Add(newContact);
        await db.SaveChangesAsync(cancellationToken);
        return newContact.Id;
    }

    public Task<bool> RemoveContact(int studyId, int contactId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}