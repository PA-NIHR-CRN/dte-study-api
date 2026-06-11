using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.VolunteerInformation.Data;

public interface IStudyRepository
{
    Task<Study?> GetStudy(int studyId, CancellationToken cancellationToken);
}

public class StudyDbRepository(ParticipantDbContext db) : IStudyRepository
{
    public async Task<Study?> GetStudy(int studyId, CancellationToken cancellationToken)
    {
        return await db.Studies.SingleOrDefaultAsync(i => i.Id == studyId && !i.IsDeleted,
            cancellationToken: cancellationToken);
    }
}

public class TestStudyRepository : IStudyRepository
{
    public Task<Study?> GetStudy(int studyId, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            studyId switch
            {
                154 => new Study
                {
                    Id = studyId,
                    StudyName = "Investigation into transmission of the common cold from Veliociraptor Mongoliensis"
                },
                _ => null
            });
    }
}