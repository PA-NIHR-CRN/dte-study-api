
using BPOR.Domain.Entities;
using BPOR.Domain.Entities.Configuration;
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

            participantLocation.SetLocation(lat, lon);

            participantLocation.Location.Should().Be(point);
            participantLocation.Easting.Should().Be(easting);
            participantLocation.Northing.Should().Be(northing);
        }

        [Fact]
        public void SetLocationPointIsCorrect()
        {
            ParticipantLocation participantLocation = new ParticipantLocation();

            participantLocation.SetLocation(point);

            participantLocation.Location.Should().Be(point);
            participantLocation.Easting.Should().Be(easting);
            participantLocation.Northing.Should().Be(northing);
        }

        [Fact]
        public void SetLocationCoordinatesIsCorrect()
        {
            ParticipantLocation participantLocation = new ParticipantLocation();

            participantLocation.SetLocation(coordinatesModel);

            participantLocation.Location.Should().Be(point);
            participantLocation.Easting.Should().Be(easting);
            participantLocation.Northing.Should().Be(northing);
        }

        [Fact]
        public void FronmLatLongIsCorrect()
        {
            ParticipantLocation participantLocation = ParticipantLocation.FromLatLong(lat, lon);

            participantLocation.Location.Should().Be(point);
            participantLocation.Easting.Should().Be(easting);
            participantLocation.Northing.Should().Be(northing);
        }

        [Fact]
        public void FromPointIsCorrect()
        {
            ParticipantLocation participantLocation = ParticipantLocation.FromPoint(point);

            participantLocation.Location.Should().Be(point);
            participantLocation.Easting.Should().Be(easting);
            participantLocation.Northing.Should().Be(northing);
        }

        [Fact]
        public void FromCoordinatesIsCorrect()
        {
            ParticipantLocation participantLocation = ParticipantLocation.FromCoordinates(coordinatesModel);

            participantLocation.Location.Should().Be(point);
            participantLocation.Easting.Should().Be(easting);
            participantLocation.Northing.Should().Be(northing);
        }
    }
}
