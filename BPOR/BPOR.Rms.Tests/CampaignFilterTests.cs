using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using BPOR.Rms.Services;
using BPOR.Tests.Common;
using BPOR.Tests.Common.Fakers;
using FluentAssertions;
using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure;
using NSubstitute;

namespace BPOR.Rms.Tests
{
    public class CampaignTestDataSetFixture : LocalParticipantDatabaseFixture
    {
        public CampaignTestDataSetFixture()
        {
            using var participantDbContext = LocalParticipantDatabase.CreateDbContext();

            var faker = new ParticipantFaker();
            faker.UseSeed(5832);
            var participants = faker.Generate(10000);
            participantDbContext.Participants.AddRange(participants);
            participantDbContext.SaveChanges();
        }
    }
    
    public class CampaignFilterTests(CampaignTestDataSetFixture databaseFixture) : IClassFixture<CampaignTestDataSetFixture>
    {
        [Fact, Trait("RequiresMySql", "true")]
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

        [Fact, Trait("RequiresMySql", "true")]
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

        [Fact, Trait("RequiresMySql", "true")]
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

        [Fact, Trait("RequiresMySql", "true")]
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

        [Fact, Trait("RequiresMySql", "true")]
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

        [Fact, Trait("RequiresMySql", "true")]
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
            var chiSqPValue =  MathNet.Numerics.Distributions.ChiSquared.CDF(actualResult.Count - 1, chiSqValue);

            chiSqPValue.Should().BeLessThan(0.10);
        }
    }
}
