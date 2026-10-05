using BPOR.Domain.Entities;

namespace BPOR.Rms.Ms4.Repositories;

public interface IStudyDraftRepository
{
    Task<Study?> GetStudyAsync(int id, CancellationToken cancellationToken);
    Task<int> CreateDraftStudyAsync(Study study, CancellationToken cancellationToken);
    Task SaveStudyAsync(Study study, CancellationToken cancellationToken);
    Task SubmitStudyAsync(int id, CancellationToken cancellationToken);
}