using System.Linq.Expressions;
using System.Text.Json;
using BPOR.Rms.Abstractions.Entities;
using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.VolunteerInformation.Utility;
using Microsoft.Extensions.Caching.Memory;

namespace BPOR.Rms.VolunteerInformation.Data;

public abstract class VipFileRepository(IMemoryCache cache) : IVipRepository
{
    private readonly TimeSpan _cacheTtl = TimeSpan.FromMinutes(5);

    protected abstract Task<Stream?> OpenReadStream(long studyId, CancellationToken cancellationToken);
    protected abstract Task<Stream> OpenWriteStream(long studyId, CancellationToken cancellationToken);

    private async Task<VsiPage?> Load(long studyId, CancellationToken cancellationToken)
    {
        return await cache.GetOrCreateAsync(new CacheKey(studyId), async cacheEntry =>
        {
            
            await using var stream = await OpenReadStream(studyId, cancellationToken);
            cacheEntry.AbsoluteExpirationRelativeToNow = _cacheTtl;
            var jsonSerializerOptions = new JsonSerializerOptions();
            var result = stream == null
                ? null
                : await JsonSerializer.DeserializeAsync<VsiPage>(stream, jsonSerializerOptions,
                    cancellationToken: cancellationToken);
            return result;
        });
    }

    private async Task Save(long studyId, VsiPage data, CancellationToken cancellationToken)
    {
        await using var stream = await OpenWriteStream(studyId, cancellationToken);
        await JsonSerializer.SerializeAsync(stream, data, cancellationToken: cancellationToken);
        cache.Set(new CacheKey(studyId), data, _cacheTtl);
    }

    private record CacheKey(long StudyId);

    public Task<long?> GetActiveDraftId(int studyId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<VsiPage?> GetPage(int studyId, CancellationToken cancellationToken)
    {
        return Load(studyId, cancellationToken);
    }

    public async Task<T?> GetPage<T>(long studyId, Expression<Func<VsiPage, T>> selector,
        CancellationToken cancellationToken)
    {
        var vsi = await Load(studyId, cancellationToken);
        return vsi == null ? default : selector.Compile()(vsi);
    }

    public async Task<T?> GetGroup<T>(long studyId, long groupId,
        Expression<Func<VsiGroup, T>> selector, CancellationToken cancellationToken)
    {
        var vsi = await Load(studyId, cancellationToken);
        var group = vsi?.Groups.SingleOrDefault(g => g.Id == groupId);
        return group == null ? default : selector.Compile()(group);
    }

    public async Task<bool> RemoveGroup(int studyId, int groupId, CancellationToken cancellationToken)
    {
        var vsi = await Load(studyId, cancellationToken);
        var group = vsi?.Groups.SingleOrDefault(g => g.Id == groupId);

        if (group == null)
        {
            return false;
        }

        vsi!.Groups.Remove(group);
        await Save(studyId, vsi, cancellationToken);
        return true;
    }

    public async Task<bool> RemoveCriteria(int studyId, int groupId, int criteriaId,
        CancellationToken cancellationToken)
    {
        var vsi = await Load(studyId, cancellationToken);
        var group = vsi?.Groups.SingleOrDefault(g => g.Id == groupId);
        var criteria = group?.Criteria.SingleOrDefault(c => c.Id == criteriaId);

        if (criteria == null)
        {
            return false;
        }

        group!.Criteria.Remove(criteria);
        await Save(studyId, vsi!, cancellationToken);
        return true;
    }

    public async Task<bool> CreateCriterion(int studyId, int groupId,
        VsiGroupCriterion newCriteria, CancellationToken cancellationToken)
    {
        return await UpdateGroup(studyId, groupId, group =>
        {
            newCriteria.Id = group.Criteria.MaxOrDefault(i => i.Id) + 1;
            group.Criteria.Add(newCriteria);
        }, cancellationToken);
    }

    public async Task<bool> UpdateVsi(int studyId, Action<VsiPage> action,
        CancellationToken cancellationToken)
    {
        return await UpdateVsi(
            studyId,
            vsi =>
            {
                action(vsi);
                return true;
            },
            cancellationToken);
    }

    public async Task<T?> UpdateVsi<T>(int studyId, Func<VsiPage, T> action,
        CancellationToken cancellationToken)
    {
        var vsi = await Load(studyId, cancellationToken);
        if (vsi == null)
        {
            return default;
        }

        var result = action(vsi);
        await Save(studyId, vsi, cancellationToken);
        return result;
    }

    public async Task<bool> RemoveSite(int studyId, int siteId, CancellationToken cancellationToken)
    {
        var vsi = await Load(studyId, cancellationToken);
        if (vsi?.Sites.RemoveWhere(i => i.Id == siteId) == true)
        {
            await Save(studyId, vsi, cancellationToken);
            return true;
        }

        return false;
    }

    public async Task<int?> CreateGroup(int studyId, string groupName,
        CancellationToken cancellationToken)
    {
        return (await UpdateVsi(studyId, vsi =>
        {
            VsiGroup group = new()
            {
                Name = groupName,
                Id = vsi.Groups.MaxOrDefault(i => i.Id) + 1
            };
            vsi.Groups.Add(group);
            return group;
        }, cancellationToken))?.Id;
    }

    public async Task<int?> CreateSite(int studyId, VsiSite newSite,
        CancellationToken cancellationToken)
    {
        return (await UpdateVsi(studyId, vsi =>
        {
            newSite.Id = vsi.Sites.MaxOrDefault(i => i.Id) + 1;
            vsi.Sites.Add(newSite);
            return newSite;
        }, cancellationToken))?.Id;
    }

    public async Task CreatePage(int studyId, VsiStatus status, string? preFilledPrescreenerUrl,
        CancellationToken cancellationToken)
    {
        VsiPage vsi = new VsiPage()
        {
            Status = status,
            PreScreenerUrl = preFilledPrescreenerUrl
        };
        await Save(studyId, vsi, cancellationToken);
    }

    public async Task CreatePage(int studyId, VsiPage data, CancellationToken cancellationToken)
    {
        await Save(studyId, data, cancellationToken);
    }

    public async Task ResetPage(int studyId, string? preFilledPrescreenerUrl, CancellationToken cancellationToken)
    {
        await CreatePage(studyId, VsiStatus.Draft, preFilledPrescreenerUrl, cancellationToken);
    }

    public async Task<int?> CreateContact(int studyId, VsiContact newContact, CancellationToken cancellationToken)
    {
        return (await UpdateVsi(studyId, vsi =>
        {
            newContact.Id = vsi.Contacts.MaxOrDefault(i => i.Id) + 1;
            vsi.Contacts.Add(newContact);
            return newContact;
        }, cancellationToken))?.Id;
    }

    public async Task<bool> RemoveContact(int studyId, int contactId, CancellationToken cancellationToken)
    {
        var vsi = await Load(studyId, cancellationToken);
        if (vsi?.Contacts.RemoveWhere(i => i.Id == contactId) == true)
        {
            await Save(studyId, vsi, cancellationToken);
            return true;
        }

        return false;
    }

    public async Task<VsiStatus?> GetVipStatus(int studyId, CancellationToken cancellationToken)
    {
        var vsi = await Load(studyId, cancellationToken);
        return vsi?.Status;
    }

    public async Task<bool> UpdateGroup(int studyId, int groupId, Action<VsiGroup> action,
        CancellationToken cancellationToken)
    {
        var vsi = await Load(studyId, cancellationToken);
        var group = vsi?.Groups.SingleOrDefault(g => g.Id == groupId);
        if (group == null)
        {
            return false;
        }

        action(group);
        await Save(studyId, vsi, cancellationToken);
        return true;
    }
}