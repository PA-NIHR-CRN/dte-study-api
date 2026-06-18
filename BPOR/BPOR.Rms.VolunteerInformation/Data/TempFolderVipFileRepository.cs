using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BPOR.Rms.VolunteerInformation.Data;

public class TempFolderVipFileRepository (IMemoryCache memoryCache) : VipFileRepository(memoryCache)
{
    protected override async Task<Stream?> OpenReadStream(long studyId, CancellationToken cancellationToken)
    {
        string path = GetPath(studyId);
        return File.Exists(path) ? // It is exceptional behaviour for the file is deleted between now and opening the read stream.
            File.OpenRead(path) : null;
    }

    private string GetPath(long studyId)
    {
        string folder = Path.Combine(Path.GetTempPath(), "__rms_vsi");
        Directory.CreateDirectory(folder);
        return Path.Combine(folder, $"vsi_{studyId}.json");
    }

    protected override async Task<Stream?> OpenWriteStream(long studyId, CancellationToken cancellationToken)
    {
        string path = GetPath(studyId);
        return File.Create(path);
    }
}