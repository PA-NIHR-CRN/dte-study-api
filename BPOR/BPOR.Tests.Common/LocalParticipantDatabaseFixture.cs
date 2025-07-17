using BPOR.Domain.Entities;

namespace BPOR.Tests.Common
{
    public class LocalParticipantDatabaseFixture : IDisposable
    {
        public LocalParticipantDatabaseFixture()
        {
            var configuration = TestConfiguration.GetStandardConfiguration();
            LocalParticipantDatabase = new LocalParticipantDatabase(configuration);
        }

        public void Dispose()
        {
            LocalParticipantDatabase.Dispose();
        }

        public LocalParticipantDatabase LocalParticipantDatabase { get; }
    }
}
