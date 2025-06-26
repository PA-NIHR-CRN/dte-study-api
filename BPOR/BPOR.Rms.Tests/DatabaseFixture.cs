using BPOR.Registration.Stream.Handler.Tests;
using BPOR.Tests.Common;

namespace BPOR.Rms.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            var configuration = TestConfiguration.GetStandardConfiguration();
            this.LocalParticipantDatabase = new LocalParticipantDatabase(configuration);

            using var context = LocalParticipantDatabase.CreateDbContext();
            var faker = new ParticipantFaker();
            faker.UseSeed(5832);
            var participants = faker.Generate(10000);
            context.Participants.AddRange(participants);
            context.SaveChanges();
        }

        public void Dispose()
        {
            LocalParticipantDatabase.Dispose();
        }

        public LocalParticipantDatabase LocalParticipantDatabase { get; set; }

    }
}
