using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using BPOR.Rms.Tests.Utilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.Geometries;
using NIHR.NotificationService.Tests.Fixtures;

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
            ParticipantDatabaseFixture fixture = new ParticipantDatabaseFixture(_configuration);
            using var db = fixture.CreateContext();

            static Participant CreateParticipant(string name, double latitude, double longitude, int easting, int northing)
                => new Participant
                {
                    FirstName = name,
                    ParticipantLocation = new ParticipantLocation
                    {
                        Location = new Point(longitude, latitude) { SRID = 4326 },
                        Easting = easting,
                        Northing = northing
                    }
                };

            // Define participants for UK stations (easy to obtain data set!)
            var KGL = CreateParticipant("KGL", 51.706373, -0.43814265, 508019, 202003); // King's Langley
            var VIC = CreateParticipant("VIC", 51.498148, -0.14262605, 529026, 179325); // London Victoria
            var KGX = CreateParticipant("KGX", 51.532722, -0.12327559, 530270, 183204); // London King's Cross
            var BCU = CreateParticipant("BCU", 50.816670, -1.5737827, 430122, 101989); // Brockenhurst
            var BTN = CreateParticipant("BTN", 50.830229, -0.14145174, 530984, 105056); // Brighton - 46.5 miles South of VIC
            var SWI = CreateParticipant("SWI", 51.565723, -1.7856254, 414956, 185227); // Swindon - 70.5 miles West of VIC

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
