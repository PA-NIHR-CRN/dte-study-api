using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Registration.Stream.Handler.Tests;
using BPOR.Rms.Models.Filter;
using BPOR.Rms.Services;
using BPOR.Rms.Utilities.Interfaces;
using BPOR.Tests.Common;
using FluentAssertions;
using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Options;
using MySqlConnector;
using NIHR.Infrastructure;
using NIHR.Infrastructure.Interfaces;
using NSubstitute;
using static BPOR.Tests.Common.LocalParticipantDatabase;

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

    public class CampaignFilterTests(DatabaseFixture databaseFixture) : IClassFixture<DatabaseFixture>
    {

        [Fact]
        public async Task FullSetIsReturnedWhenOverDemanded()
        {
           using var participantDbContext = databaseFixture.LocalParticipantDatabase.CreateDbContext();

            var options = Options.Create(new VolunteerFilterServiceOptions
            {
                InitialPageSize = 0.2
            });

            var volunteerFilterService = new VolunteerFilterService(
                    new FakeLogger<VolunteerFilterService>(),
                    participantDbContext,
                    TimeProvider.System,
                    Substitute.For<IPostcodeMapper>(),
                    options
                );


            var result = await volunteerFilterService.GetFilteredVolunteersAsync(new FilterCriteria(), 12000, CancellationToken.None);
            result.Should().HaveCount(10000);

        }

        [Fact]
        public async Task PartialSetIsReturnedWhenUnderDemanded()
        {
            using var participantDbContext = databaseFixture.LocalParticipantDatabase.CreateDbContext();

            var options = Options.Create(new VolunteerFilterServiceOptions
            {
                InitialPageSize = 0.2
            });

            var volunteerFilterService = new VolunteerFilterService(
                    new FakeLogger<VolunteerFilterService>(),
                    participantDbContext,
                    TimeProvider.System,
                    Substitute.For<IPostcodeMapper>(),
                    options
                );


            var result = await volunteerFilterService.GetFilteredVolunteersAsync(new FilterCriteria(), 5000, CancellationToken.None);
            result.Should().HaveCount(5000);
        }

        [Fact]
        public async Task FullCountIsReturned()
        {
            using var participantDbContext = databaseFixture.LocalParticipantDatabase.CreateDbContext();

            var options = Options.Create(new VolunteerFilterServiceOptions
            {
                FilterCountBatchSize = 1000
            });

            var volunteerFilterService = new VolunteerFilterService(
                    new FakeLogger<VolunteerFilterService>(),
                    participantDbContext,
                    TimeProvider.System,
                    Substitute.For<IPostcodeMapper>(),
                    options
                );


            var result = await volunteerFilterService.GetFilteredVolunteerCountAsync(new VolunteerFilterViewModel(), CancellationToken.None);
            result.Should().Be(10000);
        }

    }
}
