using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using BPOR.Rms.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure;
using NSubstitute;

namespace BPOR.Rms.Tests
{

    public class CampaignFilterTests(DatabaseFixture databaseFixture) : IClassFixture<DatabaseFixture>
    {

        [MySqlFact]
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

        [MySqlFact]
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

        [MySqlFact]
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

        [MySqlFact]
        public async Task CorrectSubsetCounted()
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

            var result = await volunteerFilterService.GetFilteredVolunteerCountAsync(new VolunteerFilterViewModel()
            {
                SelectedVolunteersCompletedRegistration = true
            }, CancellationToken.None);

            var correctCount = participantDbContext.Participants.Where(i => i.Stage2CompleteUtc != null).Count();
            result.Should().Be(correctCount);
        }

        [MySqlFact]
        public async Task CorrectSubsetFetched()
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

            var result = await volunteerFilterService.GetFilteredVolunteersAsync(
                new FilterCriteria { IncludeCompletedRegistration = true }, null, CancellationToken.None);

            var correctResult = participantDbContext.Participants.Where(i => i.Stage2CompleteUtc != null).ToArray();

            result.Should().HaveCount(correctResult.Length);
            result.Should().AllSatisfy(i => correctResult.Any(j => j.Id == i.Id));

        }

        [MySqlFact]
        public async Task SampleIsRandom()
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

            var actualResult = participantDbContext.Participants.Where(i => i.Stage2CompleteUtc != null).
                ToDictionary(i => i.Id, i => 0);

            int runCount = 50;
            int sampleSize = 4000;

            for (int i = 0; i < runCount; i++)
            {
                var result = await volunteerFilterService.GetFilteredVolunteersAsync(
                    new FilterCriteria { IncludeCompletedRegistration = true }, sampleSize, CancellationToken.None);

                foreach (var item in result)
                    actualResult[item.Id]++;
            }

            double[] expectedCounts = new double[actualResult.Count];
            for (int i = 0; i < actualResult.Count; i++)
                expectedCounts[i] = ((double)runCount * sampleSize) / actualResult.Count;
            
            var chiSqValue = StatsUtils.ChiSqStat(actualResult.Values.ToArray(), expectedCounts);
            var chiSqPValue = StatsUtils.ChiSqPval(chiSqValue, actualResult.Count - 1);

            chiSqPValue.Should().BeGreaterThanOrEqualTo(0.10);
        }
    }
}
