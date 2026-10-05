using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.Ms4.Repositories;

public sealed class StudyDraftRepository(ParticipantDbContext dbContext) : IStudyDraftRepository
{
    public async Task<Study?> GetStudyAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Studies
            .Include(x => x.Submitted)
            .Include(x => x.NihrFundingStatus)
            .SingleOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<int> CreateDraftStudyAsync(Study study, CancellationToken cancellationToken)
    {
        study.StudyStatusId = StudyStatusType.Draft;
        
        dbContext.Studies.Add(study);
        
        var studyStausHistory = new StudyStatusHistory
        {
            Study = study,
            StudyStatusId = StudyStatusType.Draft
        };
        
        dbContext.Add(studyStausHistory);

        await dbContext.SaveChangesAsync(cancellationToken);

        return study.Id;
    }

    public async Task SaveStudyAsync(Study study, CancellationToken cancellationToken)
    {
        dbContext.Studies.Update(study);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SubmitStudyAsync(int id, CancellationToken cancellationToken)
    {
        var study = await dbContext.Studies
            .SingleAsync(x => x.Id == id, cancellationToken);

        study.StudyStatusId = StudyStatusType.NewApplication;
        
        var studyStausHistory = new StudyStatusHistory
        {
            Study = study,
            StudyStatusId = StudyStatusType.NewApplication
        };
        
        dbContext.Add(studyStausHistory);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}