using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.StudyRegistrations;

namespace Application.Contracts
{
    public interface IStudyRegistrationRepository
    {
        Task<StudyRegistration> GetStudyRegistrationAsync(long studyId);
        Task<IEnumerable<StudyRegistration>> GetStudyRegistrationsByStatusAsync(StudyRegistrationStatus status);
        Task CreateStudyRegistrationAsync(StudyRegistration entity);
        Task SaveStudyRegistrationAsync(StudyRegistration entity);
    }
}