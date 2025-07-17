
using BPOR.Domain.Entities;
using BPOR.Domain.Entities.Configuration;
using BPOR.Domain.Extensions;
using FluentAssertions;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure.Models;

namespace BPOR.Rms.Tests
{
    public class ParticipantLocationTests
    {
        private const double lat = 54.598746;
        private const double lon = -5.9335271;
        private const int easting = 146029;
        private const int northing = 529733;

        private readonly Point point = new(lon, lat) { SRID = ParticipantLocationConfiguration.LocationSrid };
        private readonly CoordinatesModel coordinatesModel = new() { Latitude = lat, Longitude = lon };

        [Fact]
        public void SetLocationLatLongIsCorrect()
        {
            ParticipantLocation participantLocation = new ParticipantLocation();

            participantLocation.SetLocationFromLatLong(lat, lon);

            participantLocation.Location.Should().Be(point);
            participantLocation.Easting.Should().Be(easting);
            participantLocation.Northing.Should().Be(northing);
        }

        [Fact]
        public void SetLocationCoordinatesIsCorrect()
        {
            ParticipantLocation participantLocation = new ParticipantLocation();

            participantLocation.SetLocationFromLatLong(coordinatesModel);

            participantLocation.Location.Should().Be(point);
            participantLocation.Easting.Should().Be(easting);
            participantLocation.Northing.Should().Be(northing);
        }
    }
}
