using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Domain.Entities.StudyRegistrations;

namespace StudyApi.Acceptance.Tests.Stubs
{
    public class StudyRegistrationRepositoryStub : IStudyRegistrationRepository
    {
        private ConcurrentBag<StudyRegistration> _studyRegistrations;

        public StudyRegistrationRepositoryStub()
        {
            _studyRegistrations = new ConcurrentBag<StudyRegistration>();
        }
        
        public async Task<StudyRegistration> GetStudyRegistrationAsync(long studyId)
        {
            return await Task.FromResult(_studyRegistrations.FirstOrDefault(x => x.StudyId == studyId));
        }

        public Task<IEnumerable<StudyRegistration>> GetStudyRegistrationsByStatusAsync(StudyRegistrationStatus status)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateStudyRegistrationAsync(StudyRegistration entity)
        {
            _studyRegistrations.Add(entity);
            
            await Task.CompletedTask;
        }

        public async Task SaveStudyRegistrationAsync(StudyRegistration entity)
        {
            var item = _studyRegistrations.FirstOrDefault(x => x.StudyId == entity.StudyId);

            if (item == null)
            {
                throw new Exception($"{nameof(StudyRegistrationRepositoryStub)} can not find study for id: {entity.StudyId}");
            }
            
            var myBag = new ConcurrentBag<StudyRegistration>(_studyRegistrations.Except(new[] { item }));

            _studyRegistrations = myBag; // TODO - check if this works

            await Task.CompletedTask;
        }
    }
}