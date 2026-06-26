using Microsoft.Extensions.Caching.Memory;

namespace BPOR.Rms.VolunteerInformation.Data;

public class TempFolderVipFileRepository (IMemoryCache memoryCache) : VipFileRepository(memoryCache)
{
    protected override async Task<string?> Read(long studyId, CancellationToken cancellationToken)
    {
        string path = GetPath(studyId);
        return File.Exists(path) ? // It is exceptional behaviour for the file is deleted between now and opening the read stream.
            await File.ReadAllTextAsync(path, cancellationToken) : null;
    }

    private string GetPath(long studyId)
    {
        string folder = Path.Combine(Path.GetTempPath(), "__rms_vsi");
        Directory.CreateDirectory(folder);
        return Path.Combine(folder, $"vsi_{studyId}.json");
    }

    protected override async Task CreateOrUpdate(long studyId,
        string content, CancellationToken cancellationToken)
    {
        string path = GetPath(studyId);
        await File.WriteAllTextAsync(path, content, cancellationToken);
    }
}