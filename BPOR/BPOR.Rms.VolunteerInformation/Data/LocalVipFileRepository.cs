using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BPOR.Rms.VolunteerInformation.Data;

public class LocalVipFileRepository (IOptions<LocalVipFileRepositoryOptions> options, IMemoryCache memoryCache) : VipFileRepository(memoryCache)
{
    protected override async Task<string?> Read(long studyId, CancellationToken cancellationToken)
    {
        string path = GetPath(studyId);
        return File.Exists(path) ? // It is exceptional behaviour for the file is deleted between now and opening the read stream.
            await File.ReadAllTextAsync(path, cancellationToken) : null;
    }

    private string GetPath(long studyId)
    {
        return Path.Combine(options.Value.Path, $"vsi_{studyId}.json");
    }

    protected override async Task CreateOrUpdate(long studyId,
        string content, CancellationToken cancellationToken)
    {
        string path = GetPath(studyId);
        await File.WriteAllTextAsync(path, content, cancellationToken);
    }
}