using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;

namespace BPOR.Rms.VolunteerInformation.Data;

public interface IVsiRepository
{
    Task<VolunteerStudyInformation?> GetCurrentVsi(int studyId,
        CancellationToken cancellationToken);

    Task<T?> GetCurrentVsi<T>(long studyId,
        Expression<Func<VolunteerStudyInformation, T>> selector,
        CancellationToken cancellationToken);

    Task<T?> GetCurrentVsiGroup<T>(long studyId, long groupId,
        Expression<Func<VolunteerStudyInformationGroup, T>> selector,
        CancellationToken cancellationToken);

    Task<bool> RemoveGroup(int studyId, int groupid, CancellationToken cancellationToken);
    Task<bool> RemoveCriteria(int studyId, int groupId, int criteriaId, CancellationToken cancellationToken);
    Task<bool> CreateCriterion(int studyId, int groupId, VolunteerStudyInformationGroupCriteria newCriteria, 
        CancellationToken cancellationToken);
    
    Task<bool> UpdateVsi(int studyId, Action<VolunteerStudyInformation> action, CancellationToken cancellationToken);
    Task<bool> RemoveSite(int studyId, int siteId, CancellationToken cancellationToken);
    Task<int?> CreateGroup(int studyId, string groupName, CancellationToken cancellationToken);
    Task<int?> CreateSite(int studyId, VolunteerStudyInformationSite newSite, CancellationToken cancellationToken);
    Task CreateVsi(int studyId, VolunteerStudyInformationStatusId status, CancellationToken cancellationToken);

    Task<int?> CreateContact(int studyId, VolunteerStudyInformationContact newContact,
        CancellationToken cancellationToken);
    Task<bool> RemoveContact(int studyId, int contactId, CancellationToken cancellationToken);
}