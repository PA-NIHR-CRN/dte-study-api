using System.Linq.Expressions;
using BPOR.Domain.Entities;
using BPOR.Rms.Abstractions.Entities;
using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.VolunteerInformation.Data;

public interface IVipRepository
{
    Task<VsiPage?> GetPage(int studyId,
        CancellationToken cancellationToken);

    Task<T?> GetPage<T>(long studyId,
        Expression<Func<VsiPage, T>> selector,
        CancellationToken cancellationToken);

    Task<T?> GetGroup<T>(long studyId, long groupId,
        Expression<Func<VsiGroup, T>> selector,
        CancellationToken cancellationToken);

    Task<bool> RemoveGroup(int studyId, int groupid, CancellationToken cancellationToken);
    Task<bool> RemoveCriteria(int studyId, int groupId, int criteriaId, CancellationToken cancellationToken);
    Task<bool> CreateCriterion(int studyId, int groupId, VsiGroupCriterion newCriteria, 
        CancellationToken cancellationToken);
    
    Task<bool> UpdateVsi(int studyId, Action<VsiPage> action, CancellationToken cancellationToken);

    Task<bool> UpdateVsiIfExists(int studyId, Action<VsiPage> action, CancellationToken cancellationToken);

    Task<bool> RemoveSite(int studyId, int siteId, CancellationToken cancellationToken);
    Task<int?> CreateGroup(int studyId, string groupName, CancellationToken cancellationToken);
    Task<int?> CreateSite(int studyId, VsiSite newSite, CancellationToken cancellationToken);
    Task CreatePage(Study study, VsiStatus status, CancellationToken cancellationToken);
    Task CreatePage(int studyId, VsiPage data, CancellationToken cancellationToken);
    
    Task ResetPage(Study study, CancellationToken cancellationToken);

    Task<int?> CreateContact(int studyId, VsiContact newContact,
        CancellationToken cancellationToken);
    Task<bool> RemoveContact(int studyId, int contactId, CancellationToken cancellationToken);
    Task<VsiStatus?> GetVipStatus(int studyId, CancellationToken cancellationToken);
    
    async Task<bool> IsVipActive(int studyId, CancellationToken cancellationToken)
        => (await GetVipStatus(studyId, cancellationToken)) == VsiStatus.Active;
}