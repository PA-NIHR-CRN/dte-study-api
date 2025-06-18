using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
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
            ParticipantDatabaseFixture fixture = new ParticipantDatabaseFixture(_configuration);
            using var db = fixture.CreateContext();

            Participant CreateParticipant(string name, double latitude, double longitude, int easting, int northing)
                => new Participant
                {
                    FirstName = name,
                    ParticipantLocation = new ParticipantLocation
                    {
                        Location = new Point(longitude, latitude),
                        Easting = easting,
                        Northing = northing
                    }
                };

            var KGL = CreateParticipant("KGL", 51.706373, -0.43814265, 508019, 202003);
            var VIC = CreateParticipant("VIC", 51.498148, -0.14262605, 529026, 179325);
            var KGX = CreateParticipant("KGX", 51.532722, -0.12327559, 530270, 183204);
            var BCU = CreateParticipant("BCU", 50.816670, -1.5737827, 430122, 101989);
            var BTN = CreateParticipant("BTN", 50.830229, -0.14145174, 530984, 105056);

            db.Participants.AddRange(KGL, VIC, KGX, BCU, BTN);
            await db.SaveChangesAsync();

            var within10OfVIC = await ParticipantQueryExtensions
                .WhereWithinRadiusOfLocation(db.Participants, new PostcodeRadiusSearchModel {Location = VIC.ParticipantLocation.Location, SearchRadiusMiles = 20 }).ToArrayAsync();
            Assert.Equal(2, within10OfVIC.Length);
            Assert.Contains(VIC, within10OfVIC);
            Assert.Contains(KGX, within10OfVIC);
        }
    }
}
