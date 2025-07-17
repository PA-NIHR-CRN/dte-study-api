using BPOR.Domain.Entities;
using BPOR.Domain.Extensions;
using BPOR.Rms.Models.Filter;
using BPOR.Tests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BPOR.Rms.Tests
{
    public class VolunteerFilterTests
    {
        private IConfigurationRoot _configuration;

        public VolunteerFilterTests()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.user.json", true)
                .Build();
        }

        [Fact]
        public async Task PostcodeRadiusSearchIncludesCorrectParticipants()
        {
            // Arrange
            using LocalParticipantDatabase localParticipantDatabase = new LocalParticipantDatabase(_configuration);
            using var db = localParticipantDatabase.CreateDbContext();

            static Participant CreateParticipant(string name, double latitude, double longitude)
            {
                var participantLocation = new ParticipantLocation();
                participantLocation.SetLocationFromLatLong(latitude, longitude);
                return new Participant
                {
                    FirstName = name,
                    ParticipantLocation = participantLocation
                };
            }

            // Define participants for UK stations (easy to obtain data set!)
            var KGL = CreateParticipant("KGL", 51.706373, -0.43814265); // King's Langley
            var VIC = CreateParticipant("VIC", 51.498148, -0.14262605); // London Victoria
            var KGX = CreateParticipant("KGX", 51.532722, -0.12327559); // London King's Cross
            var BCU = CreateParticipant("BCU", 50.816670, -1.5737827); // Brockenhurst
            var BTN = CreateParticipant("BTN", 50.830229, -0.14145174); // Brighton - 46.5 miles South of VIC
            var SWI = CreateParticipant("SWI", 51.565723, -1.7856254); // Swindon - 70.5 miles West of VIC

            db.Participants.AddRange(KGL, VIC, KGX, BCU, BTN, SWI);
            await db.SaveChangesAsync();

            async Task PerformTest(Participant searchOrigin, int searchRadiusMiles, params Participant[] expectedResult)
            {
                // Act
                var queryResult = await ParticipantQueryExtensions
                    .WhereWithinRadiusOfLocation(db.Participants, new PostcodeRadiusSearchModel { Location = searchOrigin.ParticipantLocation.Location, SearchRadiusMiles = searchRadiusMiles }).ToArrayAsync();

                // Assert
                queryResult.Should().OnlyContain(expectedResult);
            }

            await PerformTest(VIC, 10, KGX, VIC);
            await PerformTest(VIC, 18, KGX, VIC);
            await PerformTest(VIC, 20, VIC, KGX, KGL);
            await PerformTest(BTN, 46, BTN);
            await PerformTest(BTN, 47, VIC, BTN);
            await PerformTest(BTN, 50, VIC, BTN, KGX);
            await PerformTest(BCU, 52, BCU);
            await PerformTest(BCU, 53, BCU, SWI);
            await PerformTest(VIC, 70, KGL, VIC, KGX, BTN);
            await PerformTest(VIC, 71, KGL, VIC, KGX, BTN, SWI);

        }
    }
}
