using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BPOR.Rms.VolunteerInformation.Data;

public class LocalVipFileRepository (IOptions<LocalVipFileRepositoryOptions> options, IMemoryCache memoryCache) : VipFileRepository(memoryCache)
{
    protected override async Task<Stream?> OpenReadStream(long studyId, CancellationToken cancellationToken)
    {
        string path = GetPath(studyId);
        return File.Exists(path) ? // It is exceptional behaviour for the file is deleted between now and opening the read stream.
            File.OpenRead(path) : null;
    }

    private string GetPath(long studyId)
    {
        return Path.Combine(options.Value.Path, $"vsi_{studyId}.json");
    }

    protected override async Task<Stream?> OpenWriteStream(long studyId, CancellationToken cancellationToken)
    {
        string path = GetPath(studyId);
        return File.Create(path);
    }
}